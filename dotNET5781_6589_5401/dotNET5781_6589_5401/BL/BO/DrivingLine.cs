using System;

namespace BO
{
    public class DrivingLine
    {
        public int NumberLine { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int Frequency { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}