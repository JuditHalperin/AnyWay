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
using System.Collections.ObjectModel;
using BLAPI;
using BO;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddLineStation.xaml
    /// </summary>
    public partial class AddLineStation : Window
    {
        static IBL bl;
        public Station StationToAdd;
        public int IndexInPath;
        public bool ToAdd = false;

        public AddLineStation(ObservableCollection<Station> path)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            Ok.IsEnabled = false;
            LineStations.ItemsSource = bl.GetStations(item => // all stations that this line does not stop at
            {
                foreach (Station station in path)
                    if (station != null && station.ID == item.ID)
                        return false;
                return true;
            }).ToList();
        }

        private void Ok_IsEnabled()
        {
            if (Index.Text.Length != 0 && StationToAdd != null)
                Ok.IsEnabled = true;
            else
                Ok.IsEnabled = false;
        }

        private void LineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StationToAdd = (Station)LineStations.SelectedItem;
            Ok_IsEnabled();
        }

        private void Index_TextChanged(object sender, TextChangedEventArgs e)
        {
            Ok_IsEnabled();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(Index.Text, out IndexInPath) || IndexInPath <= 0)
                    throw new InvalidInputException("Invalid format of index.");
                ToAdd = true;
                Close();
            }
            catch (InvalidInputException ex) { MessageBox.Show(ex.Message); }
            catch (LineException ex) { MessageBox.Show(ex.Message); }
        }        
    }
}
