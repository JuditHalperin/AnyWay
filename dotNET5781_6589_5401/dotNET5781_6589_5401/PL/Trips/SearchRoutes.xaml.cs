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

namespace PL
{
    /// <summary>
    /// Interaction logic for SearchRoutes.xaml
    /// </summary>
    public partial class SearchRoutes : Window
    {
        static IBL bl;
        string username;

        public SearchRoutes(string name)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            NotifyingDateTime dateTime = new NotifyingDateTime();
            Time.DataContext = dateTime;
            dateTime.worker.RunWorkerAsync();

            username = name;

            IEnumerable<Station> stations = bl.GetStations();
            SourceStation.ItemsSource = stations;
            TargetStation.ItemsSource = stations;
        }

        private void visibilities(object sender, SelectionChangedEventArgs e)
        {
            if (SourceStation.SelectedItem != null && TargetStation.SelectedItem != null)
                if (((Station)SourceStation.SelectedItem).ID == ((Station)TargetStation.SelectedItem).ID)
                {
                    SameStation.Visibility = Visibility.Visible;
                    NoLines.Visibility = Visibility.Hidden;
                    Titles.Visibility = Visibility.Hidden;
                    Trips.Visibility = Visibility.Hidden;
                }
                else
                {
                    SameStation.Visibility = Visibility.Hidden;

                    var trips = from DrivingBus drivingBus in bl.getPassengerTrips(((Station)SourceStation.SelectedItem).ID, ((Station)TargetStation.SelectedItem).ID)
                                let timeTillArrival = bl.timeTillArrivalToSource(drivingBus, ((Station)SourceStation.SelectedItem).ID, ((Station)TargetStation.SelectedItem).ID)
                                let timeOfJourney = bl.durationTripBetweenStations(drivingBus.NumberLine, ((Station)SourceStation.SelectedItem).ID, ((Station)TargetStation.SelectedItem).ID)
                                where timeTillArrival.Seconds != -1 // not relevant
                                select new // anonymous variable for PL trip
                                {
                                    Line = drivingBus.NumberLine,
                                    TimeTillArrival = timeTillArrival.ToString(@"hh\:mm\:ss"),
                                    TimeOfJourney = timeOfJourney.ToString(@"hh\:mm\:ss"),
                                    TotalTime = (timeTillArrival + timeOfJourney).ToString(@"hh\:mm\:ss")
                                };

                    if (trips.Count() == 0)
                    {
                        NoLines.Visibility = Visibility.Visible;
                        Titles.Visibility = Visibility.Hidden;
                        Trips.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        Trips.ItemsSource = trips.Take(10).OrderBy(item => item.TotalTime); // show first 10 results
                        NoLines.Visibility = Visibility.Hidden;
                        Titles.Visibility = Visibility.Visible;
                        Trips.Visibility = Visibility.Visible;
                    }
                }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new PassengerWindow(username).Show();
            Close();
        }

        private void Trips_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new LinesList(username, (int)Trips.SelectedItem.GetType().GetProperty("Line").GetValue(0, null), false);
            Close();
        }
    }
}
