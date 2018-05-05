using System;
using Repository.ExtensionMethods;
using Xunit;

namespace Repository.UnitTests.ExtensionMethods
{
    public class DateTimeHelperTests
    {
        [Fact]
        public void Go()
        {
            var dateTime = new DateTime(2001, 1, 10, 12, 30, 45, DateTimeKind.Utc);

            Assert.Equal(979129845000L, dateTime.GetCurrentUnixTimestampMillis());
        }
    }
}