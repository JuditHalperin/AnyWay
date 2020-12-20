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

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow(string username)
        {
            InitializeComponent();
            Username.Content = username;
        }

        private void ShowBuses_Click(object sender, RoutedEventArgs e)
        {
            new Buses.BusesList().ShowDialog();
        }

        private void ShowLines_Click(object sender, RoutedEventArgs e)
        {
            new Lines.LinesList().ShowDialog();
        }

        private void ShowStations_Click(object sender, RoutedEventArgs e)
        {
            new Stations.StationsList().ShowDialog();
        }

        private void SignOut_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new Main().Show();
            Close();
        }

        private void ChangePassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new ChangePassword("password", (string) Username.Content).ShowDialog();
        }

        private void ChangeCode_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new ChangePassword("managing code", (string) Username.Content).ShowDialog();
        }
    }

}
