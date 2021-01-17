namespace DO
{
    /// <summary>
    /// Station in the path of a line
    /// Identifier: NumberLine + ID
    /// </summary>
    public class LineStation
    {
        public int NumberLine { get; set; }
        public int ID { get; set; }
        public int PathIndex { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}