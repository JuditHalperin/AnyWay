using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DrivingBus
    {
        public int Line { get; set; }
        public DateTime Start { get; set; }
        public int PreviousStationID { get; set; }
        public DateTime PreviousStationTime { get; set; }
        public DateTime NextStationTime { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
