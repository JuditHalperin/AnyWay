using BLAPI;
using BO;
using PO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for SearchRoutes.xaml
    /// </summary>
    public partial class SearchRoutes : Window
    {
        static IBL bl;
        string username;
        BackgroundWorker worker;
        bool selectionChanged;

        public SearchRoutes(string name)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            NotifyingDateTime dateTime = new NotifyingDateTime();
            Time.DataContext = dateTime;
            dateTime.worker.RunWorkerAsync();

            worker = new BackgroundWorker();
            worker.DoWork += RediscoverRoutes;
            worker.ProgressChanged += PresentRoutse;
            worker.RunWorkerCompleted += done;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            username = name;

            IEnumerable<Station> stations = bl.GetStations();
            SourceStation.ItemsSource = stations;
            TargetStation.ItemsSource = stations;
        }

        private void RediscoverRoutes(object sender, DoWorkEventArgs e)
        {
            selectionChanged = false;
            if (selectionChanged)
                worker.CancelAsync();

            while (true) // while the selection is not changed
            {
                worker.ReportProgress(0);
                Thread.Sleep(1000); // second
            }
        }

        private void PresentRoutse(object sender, ProgressChangedEventArgs e)
        {
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
            else // show first 10 sorted results
            {
                Trips.ItemsSource = trips.Take(10).OrderBy(item => item.TotalTime);
                NoLines.Visibility = Visibility.Hidden;
                Titles.Visibility = Visibility.Visible;
                Trips.Visibility = Visibility.Visible;
            }
        }

        void done(object sender, RunWorkerCompletedEventArgs e)
        {
            return;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged = true;
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
                    worker.RunWorkerAsync();
                }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {           
            new PassengerWindow(username).Show();
            Close();
        }

        private void Trips_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new LinesList(username, (int)Trips.SelectedItem.GetType().GetProperty("Line").GetValue(Trips.SelectedItem, null), false).Show();
            Close();
        }
    }
}
