using BO;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for RemoveLineStation.xaml
    /// Remove a station from a line path
    /// Constructor gets all stations in the path
    /// (used in both AddLine and EditLine)
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
