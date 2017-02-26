using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.RepositoryDto;

namespace Repository.UnitTests
{
    [TestClass]
    public class DataPointRepositoryTests
    {
        private const string ConnectionString =
            @"server=.\SQLEXPRESS;database=WeatherStation_unitTests;Integrated Security = True";

        private DataPointRepository _repository;

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

            allValues.Should().HaveCount(1);
        }

        [TestMethod]
        public void Save_twice_is_idempotent()
        {
            _repository.Save(CreateDataPoint());
            _repository.Save(CreateDataPoint());

            var allValues = _repository.FindAll();

            allValues.Should().HaveCount(1);
        }

        [TestMethod]
        public void Get_stationIds()
        {
            _repository.Save(CreateDataPoint("s1"));
            _repository.Save(CreateDataPoint("s1"));
            _repository.Save(CreateDataPoint("s2"));

            var stationIds = _repository.GetStationIds();

            stationIds.Should().Equal(new List<string> {"s1", "s2"});
        }

        [TestMethod]
        public void GetDataPoints()
        {
            var dataPoint1 = CreateDataPoint("s1", "mySensorType1");
            _repository.Save(dataPoint1);

            _repository.Save(CreateDataPoint("s1", "mySensorType2"));
            _repository.Save(CreateDataPoint("s2", "mySensorType1"));
            _repository.Save(CreateDataPoint("s2", "mySensorType2"));

            var loadedDataPoints = _repository.GetDataPoints("s1", "mySensorType1", DateTimeRange.Unbounded);

            loadedDataPoints.Should().HaveCount(1);
            DataPoint.IdentityEquals(dataPoint1, loadedDataPoints[0]).Should().BeTrue();
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
                DateTimeRange.Create(new DateTime(2001, 1, 11), null));

            loadedDataPoints.Should().HaveCount(1);
            DataPoint.IdentityEquals(dataPoint2, loadedDataPoints[0]).Should().BeTrue();
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
                DateTimeRange.Create(null, new DateTime(2001, 1, 11)));

            loadedDataPoints.Should().HaveCount(1);
            DataPoint.IdentityEquals(dataPoint1, loadedDataPoints[0]).Should().BeTrue();
        }
        
        [TestMethod]
        public void DeleteAllByStationIdTest()
        {
            var dataPointToKeep = CreateDataPoint("s1");
            _repository.Save(dataPointToKeep);
            _repository.Save(CreateDataPoint("s2"));

            _repository.DeleteAllByStationId("s2");

            var dataPoints = _repository.FindAll();
            dataPoints.Should().HaveCount(1);
            DataPoint.IdentityEquals(dataPointToKeep, dataPoints[0]).Should().BeTrue();
        }

        [TestMethod]
        public void GetLastValues_for_sensorType_filters_on_station_and_sensor()
        {
            var dataPoint = CreateDataPoint("s1", "st1");
            _repository.Save(dataPoint);
            _repository.Save(CreateDataPoint("s2", "st1"));
            _repository.Save(CreateDataPoint("s1", "st2"));

            var foundDataPoint = _repository.GetLastValues("s1", "st1");

            DataPoint.IdentityEquals(dataPoint, foundDataPoint).Should().BeTrue();
        }

        [TestMethod]
        public void GetLastValues_for_sensorType_only_get_latest()
        {
            var dataPoint = CreateDataPoint("s1", "st1", new DateTime(2001, 2, 11));
            _repository.Save(dataPoint);
            _repository.Save(CreateDataPoint("s1", "st1", new DateTime(2001, 2, 10)));

            var foundDataPoint = _repository.GetLastValues("s1", "st1");

            DataPoint.IdentityEquals(dataPoint, foundDataPoint).Should().BeTrue();
        }

        [TestMethod]
        public void GetLastValues_for_all_known_sensors()
        {
            var dataPoint1 = CreateDataPoint("s1", SensorTypeEnum.Temperature.ToString(),
                new DateTime(2001, 2, 11));
            _repository.Save(dataPoint1);

            var dataPoint2 = CreateDataPoint("s1", SensorTypeEnum.Pressure.ToString(),
                new DateTime(2001, 2, 10));
            _repository.Save(dataPoint2);

            var foundDataPoints = _repository.GetLastValues("s1");

            foundDataPoints.Should().HaveCount(2);
            DataPoint.IdentityEquals(dataPoint1, foundDataPoints[0]).Should().BeTrue();
            DataPoint.IdentityEquals(dataPoint2, foundDataPoints[1]).Should().BeTrue();
        }

        [TestMethod]
        public void GetLastValues__for_all_known_sensors_doesnt_get_unknown_types()
        {
            var dataPoint1 = CreateDataPoint("s1", "st2");
            _repository.Save(dataPoint1);

            var foundDataPoints = _repository.GetLastValues("s1");

            foundDataPoints.Should().BeEmpty();
        }

        [TestMethod]
        public void GetSummaryTest()
        {
            _repository.Save(CreateDataPoint("s1", "st1",
                new DateTime(2001, 2, 10), 100));
            _repository.Save(CreateDataPoint("s1", "st1",
                new DateTime(2001, 2, 11), 200));
            _repository.Save(CreateDataPoint("s2", "st2",
                new DateTime(2001, 2, 12), 300));

            var summaryReport = _repository.GetSummaryReport();

            summaryReport.StationIds.Should().Equal(new List<string> {"s1", "s2"});
            summaryReport.SensorDetails.Should().HaveCount(2);
            summaryReport.SensorDetails[0].StationId.Should().Be("s1");
            summaryReport.SensorDetails[0].SensorType.Should().Be("st1");
            summaryReport.SensorDetails[0].Count.Should().Be(2);
            summaryReport.SensorDetails[0].Min.Should().Be(new DateTime(2001, 2, 10));
            summaryReport.SensorDetails[0].Max.Should().Be(new DateTime(2001, 2, 11));
        }

        [TestMethod]
        public void GetSummaryTest_ordering()
        {
            _repository.Save(CreateDataPoint("s1", "st1"));
            _repository.Save(CreateDataPoint("s2", "st1"));
            _repository.Save(CreateDataPoint("s2", "st2"));
            _repository.Save(CreateDataPoint("s1", "st2"));

            var summaryReport = _repository.GetSummaryReport();

            summaryReport.SensorDetails.Should().HaveCount(4);
            summaryReport.SensorDetails[0].StationId.Should().Be("s1");
            summaryReport.SensorDetails[0].SensorType.Should().Be("st1");

            summaryReport.SensorDetails[1].StationId.Should().Be("s1");
            summaryReport.SensorDetails[1].SensorType.Should().Be("st2");

            summaryReport.SensorDetails[2].StationId.Should().Be("s2");
            summaryReport.SensorDetails[2].SensorType.Should().Be("st1");

            summaryReport.SensorDetails[3].StationId.Should().Be("s2");
            summaryReport.SensorDetails[3].SensorType.Should().Be("st2");
        }

        private static DataPoint CreateDataPoint(
            string stationId = "MyStationId",
            string sensorType = "MyTestSensor",
            DateTime? sensorTimestampUtc = null,
            double sensorValueNumber = 10)
        {
            if (!sensorTimestampUtc.HasValue)
                sensorTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6);

            var dataPoint = new DataPoint
            {
                ReceivedTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6),
                SensorTimestampUtc = sensorTimestampUtc.Value,
                SensorType = sensorType,
                StationId = stationId,
                SensorValueNumber = sensorValueNumber
            };
            return dataPoint;
        }
    }
}