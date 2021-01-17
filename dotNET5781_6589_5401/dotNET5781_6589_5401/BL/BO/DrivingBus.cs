using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// Trip of a line
    /// Identifier: NumberLine + Start
    /// </summary>
    public class DrivingBus
    {
        public int NumberLine { get; set; }
        public DateTime Start { get; set; }
        public int PreviousStationID { get; set; }
        public TimeSpan PreviousStationTime { get; set; }
        public TimeSpan NextStationTime { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
