using System;

namespace XamarinMarvelChallenge.Utils
{
    public static class DateUtils
    {
        private static readonly DateTime _unixTime = new DateTime(1970, 1, 1);

        public static string GetCurrentTimestampInMiliseconds()
        {
            TimeSpan timeSpanDifference = GetTimeSpanDifference();
            double totalMiliSeconds = timeSpanDifference.TotalMilliseconds;
            long totalMiliSecondsToLong = Convert.ToInt64(totalMiliSeconds);
            string returnValue = totalMiliSecondsToLong.ToString();

            return returnValue;
        }

        private static TimeSpan GetTimeSpanDifference()
        {
            var currentDateTimeToUniversalTime = DateTime.Now.ToUniversalTime();
            var difference = currentDateTimeToUniversalTime - _unixTime;
            return difference;
        }
    }
}
