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
        }

        private void StationsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new StationDetails((Station)StationsList.SelectedItem).ShowDialog();
        }
    }
}
