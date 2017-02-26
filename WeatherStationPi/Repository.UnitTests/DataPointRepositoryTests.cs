using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.RepositoryDto;

namespace Repository.UnitTests
{
    [TestClass]
    public class DataPointRepositoryTests
    {
        private DataPointRepository _repository;

        private const string ConnectionString =
            @"server=.\SQLEXPRESS;database=WeatherStation_unitTests;Integrated Security = True";

        [TestInitialize]
        public void TestInitialize()
        {
            _repository = new DataPointRepository(ConnectionString);
            _repository.DeleteAll();
        }

        [TestMethod]
        public void SaveAndLoad()
        {
            var dataPoint = new DataPoint
            {
                ReceivedTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6),
                SensorTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6),
                SensorType = "MyTestSensor",
                StationId = "MyStationId"
            };

            _repository.Save(dataPoint);

            var allValues = _repository.FindAll();

            Assert.AreEqual(1, allValues.Count);
        }

        [TestMethod]
        public void Save_twice_is_idempotent()
        {
            _repository.Save(CreateDataPoint());
            _repository.Save(CreateDataPoint());

            var allValues = _repository.FindAll();

            Assert.AreEqual(1, allValues.Count);
        }

        [TestMethod]
        public void Get_stationIds()
        {
            _repository.Save(CreateDataPoint(stationId: "s1"));
            _repository.Save(CreateDataPoint(stationId: "s1"));
            _repository.Save(CreateDataPoint(stationId: "s2"));

            var stationIds = _repository.GetStationIds();

            CollectionAssert.AreEqual(new List<string> {"s1", "s2"}, stationIds);
        }

        [TestMethod]
        public void GetDataPoints()
        {
            var dataPoint1 = CreateDataPoint(stationId: "s1", sensorType: "mySensorType1");
            _repository.Save(dataPoint1);

            _repository.Save(CreateDataPoint(stationId: "s1", sensorType: "mySensorType2"));
            _repository.Save(CreateDataPoint(stationId: "s2", sensorType: "mySensorType1"));
            _repository.Save(CreateDataPoint(stationId: "s2", sensorType: "mySensorType2"));

            var loadedDataPoints = _repository.GetDataPoints("s1", "mySensorType1", DateTimeRange.Unbounded);

            Assert.AreEqual(1, loadedDataPoints.Count);
            Assert.IsTrue(DataPoint.IdentityEquals(dataPoint1, loadedDataPoints[0]));
        }

        [TestMethod]
        public void GetDataPoints_filter_by_date_has_start()
        {
            var dataPoint1 = CreateDataPoint(sensorTimestampUtc: new DateTime(2001, 1, 10));
            var dataPoint2 = CreateDataPoint(sensorTimestampUtc: new DateTime(2001, 1, 12));
            _repository.Save(dataPoint1);
            _repository.Save(dataPoint2);

            var loadedDataPoints = _repository.GetDataPoints(
                dataPoint1.StationId, 
                dataPoint1.SensorType, 
                DateTimeRange.Create(new DateTime(2001,1,11),null));

            Assert.AreEqual(1, loadedDataPoints.Count);
            Assert.IsTrue(DataPoint.IdentityEquals(dataPoint2, loadedDataPoints[0]));
        }

        [TestMethod]
        public void GetDataPoints_filter_by_date_has_end()
        {
            var dataPoint1 = CreateDataPoint(sensorTimestampUtc: new DateTime(2001, 1, 10));
            var dataPoint2 = CreateDataPoint(sensorTimestampUtc: new DateTime(2001, 1, 12));
            _repository.Save(dataPoint1);
            _repository.Save(dataPoint2);

            var loadedDataPoints = _repository.GetDataPoints(
                dataPoint1.StationId,
                dataPoint1.SensorType,
                DateTimeRange.Create(null,new DateTime(2001, 1, 11)));

            Assert.AreEqual(1, loadedDataPoints.Count);
            Assert.IsTrue(DataPoint.IdentityEquals(dataPoint1, loadedDataPoints[0]));
        }


        [TestMethod]
        public void DeleteAllByStationIdTest()
        {
            var dataPointToKeep = CreateDataPoint(stationId: "s1");
            _repository.Save(dataPointToKeep);
            _repository.Save(CreateDataPoint(stationId: "s2"));

            _repository.DeleteAllByStationId("s2");

            var dataPoints = _repository.FindAll();
            Assert.AreEqual(1, dataPoints.Count);
            Assert.IsTrue(DataPoint.IdentityEquals(dataPointToKeep, dataPoints[0]));
        }

        [TestMethod]
        public void GetLastValues_for_sensorType_filters_on_station_and_sensor()
        {
            var dataPoint = CreateDataPoint("s1", "st1");
            _repository.Save(dataPoint);
            _repository.Save(CreateDataPoint("s2", "st1"));
            _repository.Save(CreateDataPoint("s1", "st2"));

            var foundDataPoint = _repository.GetLastValues("s1", "st1");

            Assert.IsTrue(DataPoint.IdentityEquals(dataPoint, foundDataPoint));
        }

        [TestMethod]
        public void GetLastValues_for_sensorType_only_get_latest()
        {
            var dataPoint = CreateDataPoint("s1", "st1", sensorTimestampUtc: new DateTime(2001, 2, 11));
            _repository.Save(dataPoint);
            _repository.Save(CreateDataPoint("s1", "st1", sensorTimestampUtc: new DateTime(2001, 2, 10)));

            var foundDataPoint = _repository.GetLastValues("s1", "st1");

            Assert.IsTrue(DataPoint.IdentityEquals(dataPoint, foundDataPoint));
        }

        [TestMethod]
        public void GetLastValues_for_all_known_sensors()
        {
            var dataPoint1 = CreateDataPoint("s1", SensorTypeEnum.Temperature.ToString(), sensorTimestampUtc: new DateTime(2001, 2, 11));
            _repository.Save(dataPoint1);

            var dataPoint2 = CreateDataPoint("s1", SensorTypeEnum.Pressure.ToString(), sensorTimestampUtc: new DateTime(2001, 2, 10));
            _repository.Save(dataPoint2);

            var foundDataPoints = _repository.GetLastValues("s1");

            Assert.AreEqual(2,foundDataPoints.Count);
            Assert.IsTrue(DataPoint.IdentityEquals(dataPoint1, foundDataPoints[0]));
            Assert.IsTrue(DataPoint.IdentityEquals(dataPoint2, foundDataPoints[1]));
        }

        [TestMethod]
        public void GetLastValues__for_all_known_sensors_doesnt_get_unknown_types()
        {
            var dataPoint1 = CreateDataPoint("s1", "st2");
            _repository.Save(dataPoint1);

            var foundDataPoints = _repository.GetLastValues("s1");

            Assert.AreEqual(0, foundDataPoints.Count);
        }

        private static DataPoint CreateDataPoint(
            string stationId = "MyStationId",
            string sensorType = "MyTestSensor",
            DateTime? sensorTimestampUtc = null)
        {
            if (!sensorTimestampUtc.HasValue)
            {
                sensorTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6);
            }

            var dataPoint = new DataPoint
            {
                ReceivedTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6),
                SensorTimestampUtc = sensorTimestampUtc.Value,
                SensorType = sensorType,
                StationId = stationId
            };
            return dataPoint;
        }
    }
}