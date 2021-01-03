using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLAPI;
using PO;
using BO;

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
            Station station = bl.getStation(lineStation.ID);
            DataContext = lineStation;
            location.DataContext = $"({station.Latitude}°N, {station.Longitude}°E)";
            if(lineStation.PathIndex>1)
            {
                Station preStation= bl.getStation(lineStation.PreviousStationID);
                //LineStation preLineStation = bl.getLineStation(lineStation.NumberLine, preStation.ID);
                preDistance.DataContext = lineStation.LengthFromPreviousStations;
                preTime.DataContext = lineStation.TimeFromPreviousStations;
                preLocation.DataContext=$"({preStation.Latitude}°N, {preStation.Longitude}°E)";
            }
            Station p
        }
    }
}
