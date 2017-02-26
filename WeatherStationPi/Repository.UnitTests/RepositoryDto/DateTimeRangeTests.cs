using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.RepositoryDto;

namespace Repository.UnitTests.RepositoryDto
{
    [TestClass]
    public class DateTimeRangeTests
    {
        [TestMethod]
        public void Unbounded_test()
        {
            var range = DateTimeRange.Unbounded;
            Assert.IsNull(range.Start);
            Assert.IsNull(range.End);
        }
        [TestMethod]
        public void Last24Hours_test()
        {
            var range = DateTimeRange.Last24Hours;

            Assert.IsTrue(range.Start.HasValue);
            Assert.AreEqual(DateTimeKind.Utc, range.Start.Value.Kind);
            Assert.IsNull(range.End);

            Assert.IsTrue(range.Start.Value > DateTime.UtcNow -TimeSpan.FromDays(1) - TimeSpan.FromMinutes(1));
            Assert.IsTrue(range.Start.Value < DateTime.UtcNow -TimeSpan.FromDays(1) + TimeSpan.FromMinutes(1));
        }
    }
}