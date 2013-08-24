using System;

namespace WebMarket.Common
{
    public static class DateTimeExt
    {
        private static readonly TimeZoneInfo TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");

        public static DateTime ToUkrainianTimeZone(this DateTime dateTime)
        {
            if (TimeZoneInfo != null)
            {
                return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo);
            }

            return dateTime.ToUniversalTime().AddHours(3);
        }
    }
}