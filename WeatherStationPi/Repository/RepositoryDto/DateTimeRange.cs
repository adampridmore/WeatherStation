using System;

namespace Repository.RepositoryDto
{
    public class DateTimeRange
    {
        public DateTime? Start { get; }
        public DateTime? End { get; }

        private DateTimeRange(DateTime? start, DateTime? end)
        {
            Start = start;
            End = end;
        }

        public static DateTimeRange Create(DateTime? start, DateTime? end)
        {
            return new DateTimeRange(start, end);
        }
    }
}