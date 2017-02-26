using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.ExtensionMethods;

namespace Repository.UnitTests.ExtensionMethods
{
    [TestClass]
    public class DateTimeHelperTests
    {
        [TestMethod]
        public void Go()
        {
            var dateTime = new DateTime(2001, 1, 10, 12, 30, 45, DateTimeKind.Utc);

            Assert.AreEqual(979129845000L, dateTime.GetCurrentUnixTimestampMillis());
        }
    }
}