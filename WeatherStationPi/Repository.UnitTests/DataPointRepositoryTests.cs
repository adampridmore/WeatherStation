using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.RepositoryDto;

namespace Repository.UnitTests
{
    [TestClass]
    public class DataPointRepositoryTests
    {
        private const string ConnectionString =
            @"server=.\SQLEXPRESS;database=WeatherStation_unitTests;Integrated Security = True";

        [TestMethod]
        public void SaveAndLoad()
        {
            var repository = new DataPointRepository(ConnectionString);

            repository.DeleteAll();

            var dataPoint = new DataPoint
            {
                ReceivedTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6),
                SensorTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6),
                SensorType = "MyTestSensor",
                StationId = "MyStationId"
            };

            repository.Save(dataPoint);

            var allValues = repository.FindAll();

            Assert.AreEqual(1, allValues.Count);
        }

        [TestMethod]
        public void Save_twice_is_idempotent()
        {
            var repository = new DataPointRepository(ConnectionString);

            repository.DeleteAll();

            repository.Save(CreateDataPoint());
            repository.Save(CreateDataPoint());

            var allValues = repository.FindAll();

            Assert.AreEqual(1, allValues.Count);
        }

        [TestMethod]
        public void Get_stationIds()
        {
            var repository = new DataPointRepository(ConnectionString);

            repository.DeleteAll();
            repository.Save(CreateDataPoint(stationId: "s1"));
            repository.Save(CreateDataPoint(stationId: "s1"));
            repository.Save(CreateDataPoint(stationId: "s2"));

            //var stationIds = repository.GetSummaryReport().StationIds;
            var stationIds = repository.GetStationIds();

            CollectionAssert.AreEqual(new List<string> {"s1", "s2"}, stationIds);
        }

        [TestMethod]
        public void GetDataPoints()
        {
            var repository = new DataPointRepository(ConnectionString);

            repository.DeleteAll();
            var dataPoint1 = CreateDataPoint(stationId: "s1", sensorType: "mySensorType1");
            repository.Save(dataPoint1);

            repository.Save(CreateDataPoint(stationId: "s1", sensorType: "mySensorType2"));
            repository.Save(CreateDataPoint(stationId: "s2", sensorType: "mySensorType1"));
            repository.Save(CreateDataPoint(stationId: "s2", sensorType: "mySensorType2"));

            var loadedDataPoints = repository.GetDataPoints("s1", "mySensorType1", DateTimeRange.Unbounded);

            Assert.AreEqual(1, loadedDataPoints.Count);
            Assert.IsTrue(DataPoint.IdentityEquals(dataPoint1, loadedDataPoints[0]));
        }

        [TestMethod]
        public void GetDataPoints_filter_by_date_has_start()
        {
            var repository = new DataPointRepository(ConnectionString);

            repository.DeleteAll();
            var dataPoint1 = CreateDataPoint(sensorTimestampUtc: new DateTime(2001, 1, 10));
            var dataPoint2 = CreateDataPoint(sensorTimestampUtc: new DateTime(2001, 1, 12));
            repository.Save(dataPoint1);
            repository.Save(dataPoint2);

            var loadedDataPoints = repository.GetDataPoints(
                dataPoint1.StationId, 
                dataPoint1.SensorType, 
                DateTimeRange.Create(new DateTime(2001,1,11),null));

            Assert.AreEqual(1, loadedDataPoints.Count);
            Assert.IsTrue(DataPoint.IdentityEquals(dataPoint2, loadedDataPoints[0]));
        }

        [TestMethod]
        public void GetDataPoints_filter_by_date_has_end()
        {
            var repository = new DataPointRepository(ConnectionString);

            repository.DeleteAll();
            var dataPoint1 = CreateDataPoint(sensorTimestampUtc: new DateTime(2001, 1, 10));
            var dataPoint2 = CreateDataPoint(sensorTimestampUtc: new DateTime(2001, 1, 12));
            repository.Save(dataPoint1);
            repository.Save(dataPoint2);

            var loadedDataPoints = repository.GetDataPoints(
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