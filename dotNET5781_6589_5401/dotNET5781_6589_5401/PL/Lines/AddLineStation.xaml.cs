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
using System.Collections.ObjectModel;
using BLAPI;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddLineStation.xaml
    /// </summary>
    public partial class AddLineStation : Window
    {
        static IBL bl;
        public Station stationToAdd;

        public AddLineStation(ObservableCollection<Station> path)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            LineStations.ItemsSource = bl.GetStations(item => // all stations that this line does not stop at
            {
                foreach (Station station in path)
                    if (station!=null && station.ID == item.ID)
                        return false;
                return true;
            }).ToList();
        }

        private void LineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stationToAdd = (Station)LineStations.SelectedItem;
            Close();
        }
    }
}
