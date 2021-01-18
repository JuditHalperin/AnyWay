using BLAPI;
using BO;
using PO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for TripsList_Manager.xaml
    /// Trips window for a manager
    /// </summary>
    public partial class TripsList_Manager : Window
    {
        static IBL bl;
        string username;

        public TripsList_Manager(string name, int serial = -1)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            NotifyingDateTime dateTime = new NotifyingDateTime();
            Time.DataContext = dateTime;
            dateTime.worker.RunWorkerAsync();

            username = name;

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
        }

        private void ListOfLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Line)ListOfLines.SelectedItem == null)
                ListOfLines.SelectedIndex = 0;

            DataContext = (Line)ListOfLines.SelectedItem;
            Duration.Content = bl.duration(((Line)ListOfLines.SelectedItem).Path).SecondsToTimeSpan();

            IEnumerable<DrivingLine> drivinLines = bl.GetDrivingLines(item => item.NumberLine == ((Line)ListOfLines.SelectedItem).ThisSerial);
            if (drivinLines.Count() == 0)
            {
                NoTrips.Visibility = Visibility.Visible;
                Titles.Visibility = Visibility.Hidden;
                Trips.Visibility = Visibility.Hidden;
            }
            else
            {
                NoTrips.Visibility = Visibility.Hidden;
                Titles.Visibility = Visibility.Visible;
                Trips.Visibility = Visibility.Visible;
                Trips.ItemsSource = drivinLines;
            }
        }

        private void AddTrip_Click(object sender, RoutedEventArgs e)
        {
            new AddTrip(((Line)ListOfLines.SelectedItem).ThisSerial).ShowDialog();
            ListOfLines_SelectionChanged(sender, null);
        }

        private void RemoveTrip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.removeDrivingLine((DrivingLine)((Button)sender).DataContext);
                ListOfLines_SelectionChanged(sender, null);
            }
            catch (TripException ex) { MessageBox.Show(ex.Message); }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new ManagerWindow(username).Show();
            Close();
        }        
    }
}
