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
    /// Interaction logic for RemoveLineStation.xaml
    /// </summary>
    public partial class RemoveLineStation : Window
    {
        public Station StationToRemove = null;
        public RemoveLineStation(ObservableCollection<Station> path)
        {
            InitializeComponent();
            LineStations.ItemsSource = path; // all stations that this line stops at           
        }

        private void LineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationToRemove = (Station)LineStations.SelectedItem;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
