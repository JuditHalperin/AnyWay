using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class TwoFollowingStations
    {
        public int FirstStationID { get; set; }
        public int SecondStationID { get; set; }
        public double LengthBetweenStations { get; set; } // meters
        public int TimeBetweenStations { get; set; } // minutes     
        public override string ToString() => this.ToStringProperty();
    }
}
