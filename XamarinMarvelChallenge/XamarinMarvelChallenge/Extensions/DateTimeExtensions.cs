using System;

namespace XamarinMarvelChallenge.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime _unixTime = new DateTime(1970, 1, 1);

        public static string GetCurrentTimestampInMiliseconds(this DateTime thisDateTime)
        {
            var difference = thisDateTime.ToUniversalTime() - _unixTime;
            double totalMiliSeconds = difference.TotalMilliseconds;
            long totalMiliSecondsToLong = Convert.ToInt64(totalMiliSeconds);
            string returnValue = totalMiliSecondsToLong.ToString();

            return returnValue;
        }
    }
}
