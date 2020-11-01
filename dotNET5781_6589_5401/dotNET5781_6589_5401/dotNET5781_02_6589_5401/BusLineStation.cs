using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    class BusLineStation : BusStation
    {
        private double distanceFromLastStation;
        public double DistanceFromLastStation
        {
            get { return distanceFromLastStation; }
            set { distanceFromLastStation = value; }
        }

        private int minutesSinceLastStation;
        public int MinutesSinceLastStation
        {
            get { return minutesSinceLastStation; }
            set { minutesSinceLastStation = value; }
        }

        public BusLineStation(string id) : base(id) { }
    }
}
