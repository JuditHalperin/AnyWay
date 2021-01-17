using System.Collections.Generic;

namespace BO
{
    /// <summary>
    /// Material station, including lines that stop at it
    /// Identifier: ID
    /// </summary>
    public class Station
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<LineStation> LinesAtStation { get; set; }
        public override string ToString() => ID.ToString() + '\t' + Name;
    }
}
