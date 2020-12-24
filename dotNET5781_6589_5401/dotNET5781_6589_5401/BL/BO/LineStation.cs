using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation
    {
        public int NumberLine { get; set; }
        public int ID { get; set; }
        public int PathIndex { get; set; }
        public int PreviousStationID { get; set; }
        public int NextStationID { get; set; }
        public int LengthFromPreviousStations { get; set; }
        public int TimeFromPreviousStations { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}