
using FluentAssertions;
using Repository.RepositoryDto;
using System;
using Xunit;

namespace Repository.UnitTests.RepositoryDto.MongoDb
{
    public class DataPointMongoDbTests
    {
        [Fact]
        public void ToDataPointMongoDb()
        {
            var dataPoint = DataPoint.Create(
                "myStationId",
                "mySensorType",
                1,
                new DateTime(2001, 2, 3),
                new DateTime(2004, 5, 6)
                );

            var dataPointMongoDb = DataPointMongoDb.FromDataPoint(dataPoint);

            dataPointMongoDb.Id.StationId.Should().Be("myStationId");
            dataPointMongoDb.Id.SensorType.Should().Be("mySensorType");
            dataPointMongoDb.Id.SensorTimestampUtc.Should().Be(new DateTime(2001,2,3));

            dataPointMongoDb.ReceivedTimestampUtc.Should().Be(new DateTime(2004,5,6));
            dataPointMongoDb.SensorValueNumber.Should().Be(1);
        }

        [Fact]
        public void FromDataPointMongoDb()
        {
            var dataPointMongoDb = new DataPointMongoDb {
                Id = new DataPointId
                {
                    SensorTimestampUtc = new DateTime(2001, 2, 3),
                    SensorType = "mySensorType",
                    StationId = "myStationId"
                },
                SensorValueNumber = 1,
                ReceivedTimestampUtc = new DateTime(2004, 5, 6)
            };

            var dataPoint = DataPointMongoDb.ToDataPoint(dataPointMongoDb);

            dataPoint.StationId.Should().Be("myStationId");
            dataPoint.SensorType.Should().Be("mySensorType");
            dataPoint.SensorTimestampUtc.Should().Be(new DateTime(2001, 2, 3));

            dataPoint.ReceivedTimestampUtc.Should().Be(new DateTime(2004, 5, 6));
            dataPoint.SensorValueNumber.Should().Be(1);
        }
    }
}