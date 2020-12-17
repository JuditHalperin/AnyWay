using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DrivingBus
    {
        private static int serial = 1;
        public int ThisSerial { get; set; }
        public string LicensePlate { get; set; }
        public int Line { get; set; }
        public DateTime Start { get; set; }
        public DateTime ActualStart { get; set; }
        public int PreviousStationID { get; set; }
        public DateTime PreviousStationTime { get; set; }
        public DateTime NextStationTime { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
