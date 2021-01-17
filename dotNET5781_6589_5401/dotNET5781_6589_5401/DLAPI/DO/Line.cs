namespace DO
{
    /// <summary>
    /// Bus line
    /// Identifier: Serial
    /// </summary>
    public class Line
    {
        public int ThisSerial { get; set; }
        public int NumberLine { get; set; }
        public Regions Region { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}