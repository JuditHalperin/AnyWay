namespace DO
{
    /// <summary>
    /// State of a material bus
    /// </summary>
    public enum State { canDrive, cannotDrive, driving, gettingFueled, gettingServiced }

    /// <summary>
    /// Area in Israel which a line travels at
    /// </summary>
    public enum Regions { General, North, South, Center, Jerusalem }
}
