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
        public TimeSpan Start { get; set; }
        public int PreviousStationID { get; set; }
        public TimeSpan PreviousStationTime { get; set; }
        public TimeSpan NextStationTime { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
