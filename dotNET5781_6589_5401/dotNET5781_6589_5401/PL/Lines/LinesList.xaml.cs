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
    /// Interaction logic for LinesList.xaml
    /// Lines window (for both manager and passenger)
    /// </summary>
    public partial class LinesList : Window
    {
        static IBL bl;
        string username;
        bool administrativePrivileges;

        public LinesList(string name, int serial = -1, bool a = true)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            username = name;
            administrativePrivileges = a;

            List<Line> lines = bl.GetLines().ToList();
            ListOfLines.ItemsSource = lines; // it is possible to open this window only when there are lines
            
            if (serial == -1)
                ListOfLines.SelectedIndex = 0;
            else
                for (int i = 0; i < bl.countLines(); i++)
                    if (lines[i].ThisSerial == serial)
                    {
                        ListOfLines.SelectedIndex = i;
                        break;
                    }
            
            if (!administrativePrivileges)
            {
                EditLine.Visibility = Visibility.Hidden;
                RemoveLine.Visibility = Visibility.Hidden;
                AddLine.Visibility = Visibility.Hidden;
            }
        }

        private void ListOfLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Line)ListOfLines.SelectedItem == null)
                ListOfLines.SelectedIndex = 0;
            DataContext = (Line)ListOfLines.SelectedItem;
            NumberOfStations.Content = ((Line)ListOfLines.SelectedItem).Path.Count();
            if ((int)NumberOfStations.Content > 0)
            {
                NoStations.Visibility = Visibility.Hidden;
                LineStations.Visibility = Visibility.Visible;
            }
            else
            {
                LineStations.Visibility = Visibility.Hidden;
                NoStations.Visibility = Visibility.Visible;
            }
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            new AddLine().ShowDialog();
            ListOfLines.ItemsSource = bl.GetLines();
        }

        private void GroupByRegions_Click(object sender, RoutedEventArgs e)
        {
            new GroupByRegions(username, administrativePrivileges).Show();
            Close();
        }

        private void LineTrips_Click(object sender, RoutedEventArgs e)
        {
            if (administrativePrivileges)
                new TripsList_Manager(username, ((Line)ListOfLines.SelectedItem).ThisSerial).Show();
            else
                new TripsList_Passenger(username, ((Line)ListOfLines.SelectedItem).ThisSerial).Show();
            Close();
        }

        private void EditLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!bl.canChangeLine(((Line)ListOfLines.SelectedItem).ThisSerial))
                    throw new LineException("Impossible to edit a line if it is driving.");
                new EditLine((Line)ListOfLines.SelectedItem).ShowDialog();
                ListOfLines.ItemsSource = bl.GetLines();
            }
            catch (LineException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!bl.canChangeLine(((Line)ListOfLines.SelectedItem).ThisSerial))
                    throw new LineException("Impossible to remove a line if it is driving.");
                bl.removeLine((Line)ListOfLines.SelectedItem);
                if (bl.countLines() > 0)
                    ListOfLines.ItemsSource = bl.GetLines();
                else
                {
                    new ManagerWindow(username).Show();
                    Close();
                }
            }
            catch (LineException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// double click on station:
        /// left double button - open line station details
        /// right double button - open station details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LineStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(LineStations.SelectedItem != null)
                if (e.ChangedButton == MouseButton.Left)
                    new LineStationDetails((LineStation)LineStations.SelectedItem).ShowDialog();
                else // Rigth click
                {
                    new StationsList(username, administrativePrivileges, ((LineStation)LineStations.SelectedItem).ID).Show();
                    Close();
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
    }
}
