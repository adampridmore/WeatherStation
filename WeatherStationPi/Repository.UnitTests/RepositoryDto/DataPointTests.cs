using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.RepositoryDto;

namespace Repository.UnitTests.RepositoryDto
{
    [TestClass]
    public class DataPointTests
    {
        [TestMethod]
        public void ToStringTest()
        {
            var dataPoint = new DataPoint
            {
                StationId = "myStationId",
                SensorType = "mySensorType",
                SensorValueNumber = 12,
                SensorTimestampUtc = new DateTime(2001, 1, 10, 12, 45, 30),
                ReceivedTimestampUtc = new DateTime(2001, 1, 11, 12, 45, 30)
            };

            var expectedValue =
                @"StationId: myStationId, SensorType: mySensorType, SensorValueNumber: 12, SensorTimestampUtc: 10/01/2001 12:45:30, ReceivedTimestampUtc: 11/01/2001 12:45:30";

            dataPoint.ToString().Should().Be(expectedValue);
        }

        [TestMethod]
        public void CreateTest()
        {
            var dataPoint = DataPoint.Create(
                "myStationId",
                "mySensorType",
                12,
                new DateTime(2001, 1, 10),
                new DateTime(2001, 1, 11));

            dataPoint.StationId.Should().Be("myStationId");
            dataPoint.SensorType.Should().Be("mySensorType");
            dataPoint.SensorValueNumber.Should().Be(12);
            dataPoint.SensorTimestampUtc.Should().Be(new DateTime(2001, 1, 10));
            dataPoint.ReceivedTimestampUtc.Should().Be(new DateTime(2001, 1, 11));
        }

        [TestMethod]
        public void EmptyTest()
        {
            var dataPoint = DataPoint.Empty();

            dataPoint.StationId.Should().BeNull();
            dataPoint.SensorType.Should().BeNull();
            dataPoint.SensorValueNumber.Should().Be(0);
            dataPoint.ReceivedTimestampUtc.Should().Be(DateTime.MinValue);
            dataPoint.SensorTimestampUtc.Should().Be(DateTime.MinValue);
        }

        [TestMethod]
        public void SensorTypeEnumTest_Temperature()
        {
            var dataPoint = new DataPoint {SensorType = "Temperature"};

            dataPoint.SensorTypeEnum.Should().Be(SensorTypeEnum.Temperature);
        }

        [TestMethod]
        public void SensorTypeEnumTest_Unknown()
        {
            var dataPoint = new DataPoint {SensorType = "NotValid"};

            dataPoint.SensorTypeEnum.Should().BeNull();
        }

        [TestMethod]
        public void IdentityEqualsTest_for_blank()
        {
            var dp1 = new DataPoint();
            var dp2 = new DataPoint();

            DataPoint.IdentityEquals(dp1, dp2).Should().BeTrue();
            DataPoint.IdentityEquals(dp1, dp1).Should().BeTrue();
        }

        [TestMethod]
        public void IdentityEqualsTest_with_fields()
        {
            var dp1 = new DataPoint
            {
                StationId = "s1",
                SensorType = "st1",
                SensorTimestampUtc = new DateTime(2001, 1, 10)
            };
            var dp2 = new DataPoint
            {
                StationId = "s1",
                SensorType = "st1",
                SensorTimestampUtc = new DateTime(2001, 1, 10)
            };
            var dp3 = new DataPoint
            {
                StationId = "s2",
                SensorType = "st1",
                SensorTimestampUtc = new DateTime(2001, 1, 10)
            };
            var dp4 = new DataPoint
            {
                StationId = "s1",
                SensorType = "st2",
                SensorTimestampUtc = new DateTime(2001, 1, 10)
            };
            var dp5 = new DataPoint
            {
                StationId = "s1",
                SensorType = "st1",
                SensorTimestampUtc = new DateTime(2001, 1, 11)
            };

            DataPoint.IdentityEquals(dp1, dp1).Should().BeTrue();
            DataPoint.IdentityEquals(dp1, dp2).Should().BeTrue();

            DataPoint.IdentityEquals(dp1, dp3).Should().BeFalse();
            DataPoint.IdentityEquals(dp1, dp4).Should().BeFalse();
            DataPoint.IdentityEquals(dp1, dp5).Should().BeFalse();
        }

        [TestMethod]
        public void IdentityEqualsTest_for_null()
        {
            var dp1 = new DataPoint {StationId = "s1"};

            DataPoint.IdentityEquals(null, null).Should().BeTrue();
            DataPoint.IdentityEquals(dp1, null).Should().BeFalse();
            DataPoint.IdentityEquals(null, dp1).Should().BeFalse();
        }
    }
}