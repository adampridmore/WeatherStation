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