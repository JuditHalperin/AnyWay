using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class DrivingLine
    {
        public int NumberLine { get; set; }
        public TimeSpan Start { get; set; }
        public int Frequency { get; set; }
        public TimeSpan End { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}