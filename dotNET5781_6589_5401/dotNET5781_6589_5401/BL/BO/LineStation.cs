using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Station in the path of a line
    /// Identifier: NumberLine + ID
    /// </summary>
    public class LineStation
    {
        public int NumberLine { get; set; }
        public int ID { get; set; }
        public int PathIndex { get; set; }
        public int PreviousStationID { get; set; }
        public int NextStationID { get; set; }
        public int LengthFromPreviousStations { get; set; } // meters
        public int TimeFromPreviousStations { get; set; } // seconds
        public override string ToString() => ID.ToString();
    }
}