using System;

namespace Repository.ExtensionMethods
{
    public static class DateTimeHelper
    {
        public static long GetCurrentUnixTimestampMillis(this DateTime dateTime)
        {
            var UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            DateTime univDateTime;
            univDateTime = dateTime.ToUniversalTime();
            return (long) (univDateTime - UnixEpoch).TotalMilliseconds;
        }
    }
}