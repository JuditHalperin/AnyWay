using BLAPI;
using BO;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for LineStationDetails.xaml
    /// </summary>
    public partial class LineStationDetails : Window
    {
        static IBL bl;

        public LineStationDetails(LineStation lineStation)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();

            DataContext = lineStation;

            Station station = bl.getStation(lineStation.ID);
            location.DataContext = $"({station.Latitude}°N, {station.Longitude}°E)";
            
            if (lineStation.PreviousStationID != -1)
            {
                Station preStation = bl.getStation(lineStation.PreviousStationID);
                preName.DataContext = preStation.Name;
                preID.DataContext = preStation.ID;
                preDistance.DataContext = $"{lineStation.LengthFromPreviousStations / 1000.0}Km";
                preTime.DataContext = $"{lineStation.TimeFromPreviousStations / 3600:00}:{lineStation.TimeFromPreviousStations % 3600 / 60:00}:{lineStation.TimeFromPreviousStations % 3600 % 60:00}";
                preLocation.DataContext = $"({preStation.Latitude}°N, {preStation.Longitude}°E)";
            }

            else // This is the first station in the path
            {
                preName.Content = "-";
                preID.Content = "-";
                preDistance.Content = "-";
                preTime.Content = "-";
                preLocation.Content = "-";
            }

            if (lineStation.NextStationID != -1)
            {
                Station nextStation = bl.getStation(lineStation.NextStationID);
                LineStation nextLineStation = bl.getLineStation(lineStation.NumberLine, nextStation.ID);
                nextName.DataContext = nextStation.Name;
                nextID.DataContext = nextStation.ID;
                nextDistance.DataContext = $"{nextLineStation.LengthFromPreviousStations / 1000.0}Km";
                nextTime.DataContext = $"{nextLineStation.TimeFromPreviousStations / 3600:00}:{nextLineStation.TimeFromPreviousStations % 3600 / 60:00}:{nextLineStation.TimeFromPreviousStations % 3600 % 60:00}";
                nextLocation.DataContext = $"({nextStation.Latitude}°N, {nextStation.Longitude}°E)";
            }

            else // This is the last station in the path
            {
                nextName.Content = "-";
                nextID.Content = "-";
                nextDistance.Content = "-";
                nextTime.Content = "-";
                nextLocation.Content = "-";
            }
        }
    }
}
