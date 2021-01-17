namespace DO
{
    /// <summary>
    /// Two following stations: the length and the time between them
    /// Used when they follow in a line path
    /// Identifier: FirstStationID + SecondStationID
    /// </summary>
    public class TwoFollowingStations
    {
        public int FirstStationID { get; set; }
        public int SecondStationID { get; set; }
        public int LengthBetweenStations { get; set; }
        public int TimeBetweenStations { get; set; }   
        public override string ToString() => this.ToStringProperty();
    }
}