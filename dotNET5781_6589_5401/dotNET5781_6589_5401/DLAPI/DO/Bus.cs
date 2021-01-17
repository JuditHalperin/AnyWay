using System;

namespace DO
{
    /// <summary>
    /// Material bus
    /// Identifier: LicensePlate
    /// </summary>
    public class Bus
    {
        public string LicensePlate { get; set; }
        public DateTime StartOfWork { get; set; }
        public DateTime LastService { get; set; }
        public int TotalKms { get; set; }
        public int KmsSinceFuel { get; set; }
        public int KmsSinceService { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}