using System;
using Xunit;
using Repository.RepositoryDto;

namespace Repository.UnitTests.RepositoryDto
{
    
    public class DateTimeRangeTests
    {
        [Fact]
        public void Unbounded_test()
        {
            var range = DateTimeRange.Unbounded;
            Assert.Null(range.Start);
            Assert.Null(range.End);
        }

        [Fact]
        public void Last24Hours_test()
        {
            var range = DateTimeRange.Last24Hours;

            Assert.True(range.Start.HasValue);
            Assert.Equal(DateTimeKind.Utc, range.Start.Value.Kind);
            Assert.Null(range.End);

            Assert.True(range.Start.Value > DateTime.UtcNow - TimeSpan.FromDays(1) - TimeSpan.FromMinutes(1));
            Assert.True(range.Start.Value < DateTime.UtcNow - TimeSpan.FromDays(1) + TimeSpan.FromMinutes(1));
        }
    }
}