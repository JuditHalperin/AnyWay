using BLAPI;
using PO;
using System;
using System.Windows;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// Main window for a manager
    /// </summary>
    public partial class ManagerWindow : Window
    {
        static IBL bl;

        string tempUsername;

        public ManagerWindow(string username)
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
                AddFirstLine.Visibility = Visibility.Visible;
                ShowLines.Visibility = Visibility.Hidden;
                LinesSchedule.IsEnabled = false;
            }
            else
            {
                AddFirstLine.Visibility = Visibility.Hidden;
                ShowLines.Visibility = Visibility.Visible;
                LinesSchedule.IsEnabled = true;
            }

            if (bl.countStations() == 0)
            {
                AddFirstStation.Visibility = Visibility.Visible;
                ShowStations.Visibility = Visibility.Hidden;
            }
            else
            {
                AddFirstStation.Visibility = Visibility.Hidden;
                ShowStations.Visibility = Visibility.Visible;
            }
        }

        private void ShowBuses_Click(object sender, RoutedEventArgs e)
        {
            new BusesList(tempUsername).Show();
            Close();
        }

        private void ShowLines_Click(object sender, RoutedEventArgs e)
        {
            new LinesList(tempUsername).Show();
            Close();
        }

        private void ShowStations_Click(object sender, RoutedEventArgs e)
        {
            new StationsList(tempUsername).Show();
            Close();
        }
        
        private void AddFirstBus_Click(object sender, RoutedEventArgs e)
        {
            new AddBus().ShowDialog();
            enableButtons(); // refresh
        }

        private void AddFirstLine_Click(object sender, RoutedEventArgs e)
        {
            new AddLine().ShowDialog();
            enableButtons(); // refresh
        }

        private void AddFirstStation_Click(object sender, RoutedEventArgs e)
        {
            new AddStation().ShowDialog();
            enableButtons(); // refresh
        }
        private void LinesSchedule_Click(object sender, RoutedEventArgs e)
        {
            new TripsList_Manager(tempUsername).Show();
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

        private void ChangeCode_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new ChangePassword("Managing Code", tempUsername).ShowDialog();
        }

    }
}
