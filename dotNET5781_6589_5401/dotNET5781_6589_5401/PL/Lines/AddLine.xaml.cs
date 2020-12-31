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
using PO;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddLine.xaml
    /// </summary>
    public partial class AddLine : Window
    {
        static IBL bl;

        ObservableCollection<Station> path = new ObservableCollection<Station>();

        public AddLine()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            RegionsList.ItemsSource = new List<Regions> { Regions.General, Regions.North, Regions.South, Regions.Center, Regions.Jerusalem };
            LineStations.ItemsSource = path;
            RemoveStation.DataContext = path;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int line;
                if (LineNumber.Text.Length == 0 || !int.TryParse(LineNumber.Text, out line))
                    throw new InvalidInputException("Invalid format of line number.");
                if (path.Count() < 2)
                    throw new InvalidInputException("A line path should contain at least 2 stations.");
                bl.addLine(new BO.Line() { NumberLine = line, Region = (Regions)RegionsList.SelectedItem, Path = bl.convertToLineStationsList(path) });
                Close();
            }
            catch (InvalidInputException ex) { MessageBox.Show(ex.Message); }            
            catch (StationException ex) { MessageBox.Show(ex.Message); }

        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (path.Count() == bl.GetStations().Count())
                    throw new StationException("There are no more stations to add.");
                AddLineStation window = new AddLineStation(path);
                window.ShowDialog();
                path.Add(window.stationToAdd);
                Ok.IsEnabled = OkButton_IsEnabled();
            }
            catch (StationException ex) { MessageBox.Show(ex.Message); }
        }

        private void RemoveStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (path.Count() == 0)
                    throw new StationException("There are no stations to remove.");
                RemoveLineStation window = new RemoveLineStation(path);
                window.ShowDialog();
                path.Remove(window.stationToRemove);
                Ok.IsEnabled = OkButton_IsEnabled();
            }
            catch (StationException ex) { MessageBox.Show(ex.Message); }
        }

        private void LineNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            Ok.IsEnabled = OkButton_IsEnabled();
        }

        private bool OkButton_IsEnabled()
        {
            if (LineNumber.Text.Length == 0 || path.Count() < 2)
                return false;
            return true;
        }

    }
}
