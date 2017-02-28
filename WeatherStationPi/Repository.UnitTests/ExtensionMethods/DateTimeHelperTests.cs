using System;
using NUnit.Framework;
using Repository.ExtensionMethods;

namespace Repository.UnitTests.ExtensionMethods
{
    [TestFixture]
    public class DateTimeHelperTests
    {
        [Test]
        public void Go()
        {
            var dateTime = new DateTime(2001, 1, 10, 12, 30, 45, DateTimeKind.Utc);

            Assert.AreEqual(979129845000L, dateTime.GetCurrentUnixTimestampMillis());
        }
    }
}