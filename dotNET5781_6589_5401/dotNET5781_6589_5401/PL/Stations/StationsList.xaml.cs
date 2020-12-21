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

namespace PL.Stations
{
    /// <summary>
    /// Interaction logic for StationsList.xaml
    /// </summary>
    public partial class StationsList : Window
    {
        static IBL bl;

        public StationsList()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();

            StationsList.ItemsSource = bl.GetStations();
            StationsList.SelectedIndex = 0;
            DataContext = (Station)StationsList.SelectedItem;

            LinesAtStation.ItemsSource = bl.GetLineStations(item => item.ID == ((Station)DataContext).ID);
        }

        private void StationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext = (Station)StationsList.SelectedItem;
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new AddStation().ShowDialog();
        }

        private void EditStation_Click(object sender, RoutedEventArgs e)
        {
            new EditStation().ShowDialog();
        }

        private void RemoveStation_Click(object sender, RoutedEventArgs e)
        {          
            try
            {                
                IEnumerable<DrivingLine> drivingLinesAtStation = bl.GetDrivingLines(item =>
                {
                    foreach (LineStation lineStation in LinesAtStation.ItemsSource)
                        if (item.NumberLine == lineStation.NumberLine)
                            return true;
                    return false;
                });

                if (drivingLinesAtStation.Count() > 0)
                    throw new StationException("Impossible to remove a station if there are driving lines that stop there.");
                
                bl.removeStation((Station)StationsList.SelectedItem);
            }
            
            catch (StationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
