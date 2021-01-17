using System;

namespace BO
{
    /// <summary>
    /// Extension methods for conversions related to time
    /// </summary>
    public static class ExtensionTime
    {
        /// <summary>
        /// convert number of seconds (int) to TimeSpan
        /// </summary>
        /// <param name="seconds">seconds</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan SecondsToTimeSpan(this int seconds)
        {
            return new TimeSpan(seconds / 3600, seconds % 3600 / 60, seconds % 3600 % 60);
        }

        /// <summary>
        /// convert number of minutes (int) to TimeSpan
        /// </summary>
        /// <param name="minutes">minutes</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan MinutesToTimeSpan(this int minutes)
        {
            return new TimeSpan(0, minutes, 0);
        }

        /// <summary>
        /// convert TimeSpan to DateTime - today with the given time of day
        /// </summary>
        /// <param name="time">TimeSpan</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(this TimeSpan time)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, time.Hours, time.Minutes, time.Seconds);
        }
    }
}
