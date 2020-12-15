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
        public float LengthBetweenStations { get; set; } // meters
        public int TimeBetweenStations { get; set; } // minutes        
    }
}
