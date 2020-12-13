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

namespace temp
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

        }

        private void ShowLines_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowStations_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SignOut_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
