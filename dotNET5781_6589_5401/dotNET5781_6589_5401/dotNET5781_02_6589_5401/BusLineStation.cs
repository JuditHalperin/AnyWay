using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    class BusLineStation : BusStation
    {
        private double metersFromLastStation;
        public double MetersFromLastStation
        {
            get { return metersFromLastStation; }
            set
            {
                if (value < 0) // invalid distance
                    throw;

                metersFromLastStation = value;
            }
        }

        private int minutesSinceLastStation;
        public int MinutesSinceLastStation
        {
            get { return minutesSinceLastStation; }
            set
            {
                if (value < 0) // invalid time
                    throw;

                minutesSinceLastStation = value;
            }
        }

        /// <summary>
        /// constructor
        /// (use the base class contructor, and initialize its two attributes by defualt)
        /// </summary>
        /// <param name="id">station ID</param>
        public BusLineStation(string id) : base(id) { }
    }
}
