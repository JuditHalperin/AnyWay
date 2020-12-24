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

namespace PL.Lines
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
            StationsList.ItemsSource = bl.GetStations();
            StationsList.Text = ((Station)StationsList.SelectedItem).ID + " - " + ((Station)StationsList.SelectedItem).Name;
            ChosenStations.ItemsSource = path;
            Ok.IsEnabled = false;
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
                    throw new InvalidInputException("Path line should be consisted of at least 2 stations.");
                bl.addLine(new BO.Line() { NumberLine = line, Region = (Regions)RegionsList.SelectedItem, Path = bl.convertToLineStationsList(path) });
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (path.Contains((Station)StationsList.SelectedItem))
                MessageBox.Show($"Station {((Station)StationsList.SelectedItem).ID} is already in the path.");
            else
                path.Add((Station)StationsList.SelectedItem);
        }
    }
}
