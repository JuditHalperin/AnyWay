using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public static class ExtensionTime
    {
        public static TimeSpan ToTimeSpan(this int seconds)
        {
            return new TimeSpan(seconds / 3600, seconds % 3600 / 60, seconds % 3600 % 60);
        }

        public static DateTime ToDateTime(this TimeSpan time)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, time.Hours, time.Minutes, time.Seconds);
        }
    }
}
