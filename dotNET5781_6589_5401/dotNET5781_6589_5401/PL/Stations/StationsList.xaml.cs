using BLAPI;
using BO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : Window
    {
        static IBL bl;

        string username;

        bool administrativePrivileges;

        public StationsList(string name, bool a = true, int id = -1)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            username = name;
            administrativePrivileges = a;
            List<Station> stations = bl.GetStations().ToList();
            ListOfStations.ItemsSource = stations; // it is possible to open this window only when there are stations
            if (id == -1)
                ListOfStations.SelectedIndex = 0;
            else
                for (int i = 0; i < bl.countLines(); i++)
                    if (stations[i].ID == id)
                    {
                        ListOfStations.SelectedIndex = i;
                        break;
                    }

            if (!administrativePrivileges)
            {
                SetDistances.Visibility = Visibility.Hidden;
                EditStation.Visibility = Visibility.Hidden;
                RemoveStation.Visibility = Visibility.Hidden;
                AddStation.Visibility = Visibility.Hidden;
            }
        }

        private void StationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Station)ListOfStations.SelectedItem == null)
                ListOfStations.SelectedIndex = 0;
            DataContext = (Station)ListOfStations.SelectedItem;
            IEnumerable<LineStation> lineStations = ((Station)ListOfStations.SelectedItem).LinesAtStation;
            if (lineStations != null && lineStations.Count() > 0)
            {
                NoLines.Visibility = Visibility.Hidden;
                LinesAtStation.Visibility = Visibility.Visible;
                LinesAtStation.ItemsSource = lineStations;
            }
            else
            {
                LinesAtStation.Visibility = Visibility.Hidden;
                NoLines.Visibility = Visibility.Visible;
            }
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new AddStation().ShowDialog();
            ListOfStations.ItemsSource = bl.GetStations();
        }

        private void SetDistances_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Station> stations = bl.GetStations(item => // all stations that have no distance to current station
            {
                if (item.ID == ((Station)ListOfStations.SelectedItem).ID || bl.getTwoFollowingStations(item.ID, ((Station)ListOfStations.SelectedItem).ID))
                    return false;
                return true;
            });


            if (stations.Count() == 0)
                MessageBox.Show("You have set the distance to all existing stations.");
            else
                new SetDistances((Station)ListOfStations.SelectedItem, stations).ShowDialog();
        }

        private void EditStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!bl.canChangeStation((Station)ListOfStations.SelectedItem))
                    throw new StationException("Impossible to edit a station if there are driving lines that stop there.");
                new EditStation((Station)ListOfStations.SelectedItem).ShowDialog();
                ListOfStations.ItemsSource = bl.GetStations();
            }
            catch (StationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!bl.canChangeStation((Station)ListOfStations.SelectedItem))
                    throw new StationException("Impossible to remove a station if there are driving lines that stop there.");
                bl.removeStation((Station)ListOfStations.SelectedItem);
                if (bl.countStations() > 0)
                    ListOfStations.ItemsSource = bl.GetStations();
                else
                {
                    new ManagerWindow(username).Show();
                    Close();
                }
            }

            catch (StationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (administrativePrivileges)
                new ManagerWindow(username).Show();
            else
                new PassengerWindow(username).Show();
            Close();
        }

        private void LinesAtStation_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new LinesList(username, ((LineStation)LinesAtStation.SelectedItem).NumberLine, administrativePrivileges).Show();
            Close();
        }
    }
}
