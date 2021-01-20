using BLAPI;
using BO;
using PO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for SearchRoutes.xaml
    /// Get source and target stations and show the best trips (sorted)
    /// The window is updated every second (thread)
    /// </summary>
    public partial class SearchRoutes : Window
    {
        static IBL bl;
        string username;
        BackgroundWorker worker;

        public SearchRoutes(string name)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            NotifyingDateTime dateTime = new NotifyingDateTime();
            Time.DataContext = dateTime;
            dateTime.worker.RunWorkerAsync();

            worker = new BackgroundWorker();
            worker.DoWork += CheckForUpdate;
            worker.ProgressChanged += PresentRoutes;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerAsync();

            username = name;

            IEnumerable<Station> stations = bl.GetStations();
            SourceStation.ItemsSource = stations;
            TargetStation.ItemsSource = stations;
            SourceStation.SelectedItem = null;
            TargetStation.SelectedItem = null;
        }

        private void CheckForUpdate(object sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                worker.ReportProgress(0);
                Thread.Sleep(10000); // second
            }
        }

        private void PresentRoutes(object sender, ProgressChangedEventArgs e)
        {
            if (SourceStation.SelectedItem != null && TargetStation.SelectedItem != null) // both stations selected
                if (((Station)SourceStation.SelectedItem).ID == ((Station)TargetStation.SelectedItem).ID) // same station
                {
                    SameStation.Visibility = Visibility.Visible;
                    NoLines.Visibility = Visibility.Hidden;
                    Titles.Visibility = Visibility.Hidden;
                    Trips.Visibility = Visibility.Hidden;
                }
                else // present routes
                {
                    SameStation.Visibility = Visibility.Hidden;

                    var trips = (from DrivingBus drivingBus in bl.getPassengerTrips(((Station)SourceStation.SelectedItem).ID, ((Station)TargetStation.SelectedItem).ID)
                                 let timeTillArrival = bl.timeTillArrivalToSource(drivingBus, ((Station)SourceStation.SelectedItem).ID)
                                 let timeOfJourney = bl.durationTripBetweenStations(drivingBus.NumberLine, ((Station)SourceStation.SelectedItem).ID, ((Station)TargetStation.SelectedItem).ID)
                                 where timeTillArrival.Seconds != -1 // not relevant
                                 select new // anonymous variable for PL trip
                                 {
                                     Line = drivingBus.NumberLine,
                                     TimeTillArrival = timeTillArrival.ToString(@"hh\:mm\:ss"),
                                     TimeOfJourney = timeOfJourney.ToString(@"hh\:mm\:ss"),
                                     TotalTime = (timeTillArrival + timeOfJourney).ToString(@"hh\:mm\:ss")
                                 }).ToList();

                    if (trips.Count() == 0)
                    {
                        NoLines.Visibility = Visibility.Visible;
                        Titles.Visibility = Visibility.Hidden;
                        Trips.Visibility = Visibility.Hidden;
                    }
                    else // show first 10 sorted results
                    {
                        Trips.ItemsSource = trips.OrderBy(item => item.TimeTillArrival).Take(10);
                        NoLines.Visibility = Visibility.Hidden;
                        Titles.Visibility = Visibility.Visible;
                        Trips.Visibility = Visibility.Visible;
                    }
                }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new PassengerWindow(username).Show();
            worker.CancelAsync();
            Close();
        }            

        private void Trips_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Trips.SelectedItem != null)
            {
                new LinesList(username, (int)Trips.SelectedItem.GetType().GetProperty("Line").GetValue(Trips.SelectedItem, null), false).Show();
                Close();
                worker.CancelAsync();
            }
        }
    }
}
