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

            tempUsername = username;
            Username.Content = $"{BestWishesByTime()}, {username}";
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
            new Trip(tempUsername).Show();
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

        private void SignOut_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new Main().Show();
            Close();
        }

        private void ChangePassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new ChangePassword("password", tempUsername).ShowDialog();
        }     
    }
}


