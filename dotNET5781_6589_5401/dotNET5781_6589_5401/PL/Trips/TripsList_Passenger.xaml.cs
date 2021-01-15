using BLAPI;
using BO;
using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for TripsList_Passenger.xaml
    /// </summary>
    public partial class TripsList_Passenger : Window
    {
        static IBL bl;
        string username;

        public TripsList_Passenger(string name, int serial = -1)
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

            IEnumerable<TimeSpan> tripsStart = bl.getTripsStart(((Line)ListOfLines.SelectedItem).ThisSerial);
            if (tripsStart.Count() == 0)
            {
                NoTrips.Visibility = Visibility.Visible;
                TripsStart.Visibility = Visibility.Hidden;
            }
            else
            {
                NoTrips.Visibility = Visibility.Hidden;
                TripsStart.Visibility = Visibility.Visible;
                TripsStart.ItemsSource = tripsStart;
            }
        }
        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new PassengerWindow(username).Show();
            Close();
        }

    }
}
