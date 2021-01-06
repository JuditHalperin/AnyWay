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
        public int LengthBetweenStations { get; set; }
        public int TimeBetweenStations { get; set; }   
        public override string ToString() => this.ToStringProperty();
    }
}