namespace DO
{
    /// <summary>
    /// Material station
    /// Identifier: ID
    /// </summary>
    public class Station
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}