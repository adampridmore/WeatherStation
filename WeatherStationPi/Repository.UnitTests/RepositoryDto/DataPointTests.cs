using System;
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

            Assert.AreEqual(expectedValue, dataPoint.ToString());
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

            Assert.AreEqual("myStationId", dataPoint.StationId);
            Assert.AreEqual("mySensorType", dataPoint.SensorType);
            Assert.AreEqual(12, dataPoint.SensorValueNumber);
            Assert.AreEqual(new DateTime(2001, 1, 10), dataPoint.SensorTimestampUtc);
            Assert.AreEqual(new DateTime(2001, 1, 11), dataPoint.ReceivedTimestampUtc);
        }

        [TestMethod]
        public void EmptyTest()
        {
            var dataPoint = DataPoint.Empty();

            Assert.AreEqual(null, dataPoint.StationId);
            Assert.AreEqual(null, dataPoint.SensorType);
            Assert.AreEqual(0, dataPoint.SensorValueNumber);
            Assert.AreEqual(DateTime.MinValue, dataPoint.ReceivedTimestampUtc);
            Assert.AreEqual(DateTime.MinValue, dataPoint.SensorTimestampUtc);
        }

        [TestMethod]
        public void SensorTypeEnumTest_Temperature()
        {
            var dataPoint = new DataPoint {SensorType = "Temperature"};

            Assert.AreEqual(SensorTypeEnum.Temperature, dataPoint.SensorTypeEnum);
        }

        [TestMethod]
        public void SensorTypeEnumTest_Unknown()
        {
            var dataPoint = new DataPoint {SensorType = "NotValid"};

            Assert.IsNull(dataPoint.SensorTypeEnum);
        }

        [TestMethod]
        public void IdentityEqualsTest_for_blank()
        {
            var dp1 = new DataPoint();
            var dp2 = new DataPoint();

            Assert.IsTrue(DataPoint.IdentityEquals(dp1, dp2));
            Assert.IsTrue(DataPoint.IdentityEquals(dp1, dp1));
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

            Assert.IsTrue(DataPoint.IdentityEquals(dp1, dp1));
            Assert.IsTrue(DataPoint.IdentityEquals(dp1, dp2));

            Assert.IsFalse(DataPoint.IdentityEquals(dp1, dp3));
            Assert.IsFalse(DataPoint.IdentityEquals(dp1, dp4));
            Assert.IsFalse(DataPoint.IdentityEquals(dp1, dp5));
        }

        [TestMethod]
        public void IdentityEqualsTest_for_null()
        {
            var dp1 = new DataPoint {StationId = "s1"};

            Assert.IsTrue(DataPoint.IdentityEquals(null, null));
            Assert.IsFalse(DataPoint.IdentityEquals(dp1, null));
            Assert.IsFalse(DataPoint.IdentityEquals(null, dp1));
        }
    }
}