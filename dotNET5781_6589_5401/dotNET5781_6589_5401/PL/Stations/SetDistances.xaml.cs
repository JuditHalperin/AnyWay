using BLAPI;
using BO;
using PO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for SetDistances.xaml
    /// </summary>
    public partial class SetDistances : Window
    {
        static IBL bl;

        Station current;

        public SetDistances(Station station, IEnumerable<Station> stations)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            current = station;
            Stations.ItemsSource = stations;
            Ok.IsEnabled = false;
        }

        private void Ok_IsEnabled()
        {
            if (Distance.Text.Length > 0 && Stations.SelectedItem != null)
                Ok.IsEnabled = true;
            else
                Ok.IsEnabled = false;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int distance;
                if (!int.TryParse(Distance.Text, out distance))
                    throw new InvalidInputException("Invalid format of distance.");
                if (distance <= 0 || distance > 240000) // outside of Israel
                    throw new InvalidInputException("Invalid distance.");
                
                bl.addTwoFollowingStations(((Station)Stations.SelectedItem).ID, current.ID, distance);
                Close();
            }
            catch (InvalidInputException ex) { MessageBox.Show(ex.Message); }
            catch (StationException ex) { MessageBox.Show(ex.Message); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Stations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ok_IsEnabled();
        }

        private void Distance_TextChanged(object sender, TextChangedEventArgs e)
        {
            Ok_IsEnabled();
        }
    }
}
