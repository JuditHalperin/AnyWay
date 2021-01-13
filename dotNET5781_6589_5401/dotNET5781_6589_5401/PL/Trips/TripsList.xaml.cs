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
    /// Interaction logic for TripsList.xaml
    /// </summary>
    public partial class TripsList : Window
    {
        static IBL bl;
        string username;
        bool administrativePrivileges;

        public TripsList(string name, int serial = -1, bool a = true)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            username = name;
            administrativePrivileges = a;

            List<BO.Line> lines = bl.GetLines().ToList();
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

            //if (!administrativePrivileges)
            //    AddTrip.Visibility = Visibility.Hidden;
        }

        private void ListOfLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((BO.Line)ListOfLines.SelectedItem == null)
                ListOfLines.SelectedIndex = 0;
            
            DataContext = (BO.Line)ListOfLines.SelectedItem;

            IEnumerable<DrivingLine> drivinLines = bl.GetDrivingLines(item => item.NumberLine == ((BO.Line)ListOfLines.SelectedItem).ThisSerial);
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
            new AddTrip(((BO.Line)ListOfLines.SelectedItem).ThisSerial).ShowDialog();
            ListOfLines.ItemsSource = bl.GetLines();
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (administrativePrivileges)
                new ManagerWindow(username).Show();
            else
                new PassengerWindow(username).Show();
            Close();
        }
    }
}
