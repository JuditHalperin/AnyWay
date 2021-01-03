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
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : Window
    {
        static IBL bl;

        string username;

        public StationsList(string name)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            username = name;
            ListOfStations.ItemsSource = bl.GetStations(); // it is possible to open this window only when there are stations
            ListOfStations.SelectedIndex = 0;
        }
    
        private void StationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Station)ListOfStations.SelectedItem == null)
                ListOfStations.SelectedIndex = 0;
            DataContext = (Station)ListOfStations.SelectedItem;
            IEnumerable<LineStation> lineStations = ((Station)ListOfStations.SelectedItem).LinesAtStation;
            if (lineStations != null && lineStations.Count() > 0)
            {
                NoLines.Visibility = Visibility.Hidden;
                LinesAtStation.Visibility = Visibility.Visible;
                LinesAtStation.ItemsSource = lineStations;
            }
            else
            {
                LinesAtStation.Visibility = Visibility.Hidden;
                NoLines.Visibility = Visibility.Visible;
            }
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new AddStation().ShowDialog();
            ListOfStations.ItemsSource = bl.GetStations();
        }

        private void EditStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!bl.canChangeStation((Station)ListOfStations.SelectedItem))
                    throw new StationException("Impossible to edit a station if there are driving lines that stop there.");
                new EditStation((Station)ListOfStations.SelectedItem).ShowDialog();
                ListOfStations.ItemsSource = bl.GetStations();
            }
            catch (StationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveStation_Click(object sender, RoutedEventArgs e)
        {          
            try
            {
                if (!bl.canChangeStation((Station)ListOfStations.SelectedItem))
                    throw new StationException("Impossible to remove a station if there are driving lines that stop there.");
                bl.removeStation((Station)ListOfStations.SelectedItem);
                if (!bl.StationsIsEmpty())
                    ListOfStations.ItemsSource = bl.GetStations();
                else
                {
                    new ManagerWindow(username).Show();
                    Close();
                }
            }

            catch (StationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new ManagerWindow(username).Show();
            Close();
        }
    }
}
