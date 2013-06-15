using System;

namespace WebMarket.Common
{
    public static class DateTimeExt
    {
        public static DateTime ToUkrainianTimeZone(this DateTime dateTime)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            if (timeZoneInfo != null)
            {
                return TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);
            }

            return dateTime.ToUniversalTime().AddHours(2);
        }
    }
}