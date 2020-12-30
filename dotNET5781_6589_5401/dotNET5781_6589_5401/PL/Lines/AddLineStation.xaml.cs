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
    /// Interaction logic for AddLineStation.xaml
    /// </summary>
    public partial class AddLineStation : Window
    {
        static IBL bl;
        int thisSerial;
        public Station stationToAdd;

        public AddLineStation(int serial)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            thisSerial = serial;

            LineStations.ItemsSource = bl.GetStations(item => // all stations that this line does not stop at
            {
                foreach (LineStation lineStation in item.LinesAtStation)
                    if (lineStation.NumberLine != thisSerial)
                        return true;
                return false;
            }).ToList();
        }

        private void LineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stationToAdd = (Station)LineStations.SelectedItem;
            Close();
        }
    }
}
