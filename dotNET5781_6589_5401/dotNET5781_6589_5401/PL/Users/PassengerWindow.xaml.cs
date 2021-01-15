using BLAPI;
using PO;
using System;
using System.Windows;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for PassengerWindow.xaml
    /// </summary>
    public partial class PassengerWindow : Window
    {
        static IBL bl;
        string tempUsername;

        public PassengerWindow(string username)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            NotifyingDateTime dateTime = new NotifyingDateTime();
            Time.DataContext = dateTime;
            dateTime.worker.RunWorkerAsync();

            tempUsername = username;
            Username.Content = $"\t{BestWishesByTime()}, {username}";
            enableButtons();
        }

        private string BestWishesByTime()
        {
            if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12)
                return "Good Morning";
            if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                return "Good Afternoon";
            if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour < 21)
                return "Good Evening";
            return "Good Night";
        }

        private void enableButtons()
        {
            if (bl.countLines() == 0)
            {
                ShowLines.Content = "No lines";
                ShowLines.IsEnabled = false;
                AskTrips.IsEnabled = false;
                LinesSchedule.IsEnabled = false;
            }
            else
                ShowLines.Content = "Lines";

            if (bl.countStations() == 0)
            {
                ShowStations.Content = "No stations";
                ShowStations.IsEnabled = false;
            }
            else
                ShowStations.Content = "Stations";
        }

        private void Trips_Click(object sender, RoutedEventArgs e)
        {
            new SearchRoutes(tempUsername).Show();
            Close();
        }

        private void ShowLines_Click(object sender, RoutedEventArgs e)
        {
            new LinesList(tempUsername, -1, false).Show(); // -1 is defualt in the window
            Close();
        }

        private void ShowStations_Click(object sender, RoutedEventArgs e)
        {
            new StationsList(tempUsername, false).Show();
            Close();
        }
        private void LinesSchedule_Click(object sender, RoutedEventArgs e)
        {
            new TripsList_Passenger(tempUsername, -1).Show();
            Close();
        }

        private void SignOut_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new Main().Show();
            Close();
        }

        private void ChangePassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new ChangePassword("Password", tempUsername).ShowDialog();
        }

    }
}


