using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.RepositoryDto;

namespace Repository.UnitTests
{
    [TestClass]
    public class DataPointRepositoryTests
    {
        private const string ConnectionString = @"server=.\SQLEXPRESS;database=WeatherStation_unitTests;Integrated Security = True";

        [TestMethod]
        public void SaveAndLoad()
        {
            var repository = new DataPointRepository(ConnectionString);

            repository.DeleteAll();

            var dataPoint = new DataPoint
            {
                ReceivedTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6),
                SensorTimestampUtc = new DateTime(2001,2,3,4,5,6),
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

        private static DataPoint CreateDataPoint()
        {
            var dataPoint = new DataPoint
            {
                ReceivedTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6),
                SensorTimestampUtc = new DateTime(2001, 2, 3, 4, 5, 6),
                SensorType = "MyTestSensor",
                StationId = "MyStationId"
            };
            return dataPoint;
        }
    }
}
