using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// Bus line
    /// Identifier: Serial
    /// </summary>
    public class Line
    {
        public IEnumerable<LineStation> Path { get; set; }
        public int ThisSerial { get; set; }
        public int NumberLine { get; set; }
        public Regions Region { get; set; }
        public override string ToString() => ThisSerial.ToString();
    }
}
