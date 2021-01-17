using System;

namespace BO
{
    /// <summary>
    /// Trip of a line, containing details about its current station
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
