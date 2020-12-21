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

namespace PL.Stations
{
    /// <summary>
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : Window
    {
        static IBL bl;

        public StationsList()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();

            StationsList.ItemsSource = bl.GetStations();
            StationsList.SelectedIndex = 0;
            LinesAtStation.DataContext = bl.getStation(((Station)StationsList.SelectedItem).ID);
            LinesAtStation.ItemsSource = bl.GetLineStations(item => item.ID == ((Station)LinesAtStation.DataContext).ID);
        }

        private void StationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LinesAtStation.DataContext = (Station)StationsList.SelectedItem;
        }
    }
}
