using System;

namespace Repository.RepositoryDto
{
    public class DateTimeRange
    {
        private DateTimeRange(DateTime? start, DateTime? end)
        {
            Start = start;
            End = end;
        }

        public DateTime? Start { get; }
        public DateTime? End { get; }

        public static DateTimeRange Unbounded => Create(null, null);

        public static DateTimeRange Last24Hours => Create(DateTime.UtcNow - TimeSpan.FromDays(1), null);

        public static DateTimeRange Create(DateTime? start, DateTime? end)
        {
            return new DateTimeRange(start, end);
        }
    }
}