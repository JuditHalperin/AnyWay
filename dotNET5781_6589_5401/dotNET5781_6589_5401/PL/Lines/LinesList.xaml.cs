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

        private void LineStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new LineStationDetails((LineStation)LineStations.SelectedItem).ShowDialog();
        }

        private void LineStations_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //new StationsList(username, administrativePrivileges, ((LineStation)LineStations.SelectedItem).ID).Show();
            Close();
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
