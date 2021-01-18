using BLAPI;
using BO;
using PO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
                if (LineNumber.Text.Length == 0 || !int.TryParse(LineNumber.Text, out line) || line <= 0)
                    throw new InvalidInputException("Invalid format of line number.");
                
                if (path.Count() < 2)
                    throw new InvalidInputException("A line path should contain at least 2 stations.");
                
                bl.addLine(new Line() { NumberLine = line, Region = (Regions)RegionsList.SelectedItem, Path = bl.convertToLineStationsList(path) });
                Close();
            }
            catch (InvalidInputException ex) { MessageBox.Show(ex.Message); }
            catch (StationException ex) { MessageBox.Show(ex.Message); }

        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (path.Count() == bl.countStations())
                    throw new StationException("There are no more stations to add.");
                AddLineStation window = new AddLineStation(path);
                window.ShowDialog();
                if (window.ToAdd)
                {
                    int index = window.IndexInPath - 1;
                    if (index > path.Count()) index = path.Count();
                    else if (index < 0) index = 0;

                    if (index != 0 && !bl.getTwoFollowingStations(path[index - 1].ID, window.StationToAdd.ID))
                    {
                        DistanceBetweenStations innerWindow = new DistanceBetweenStations(path[index - 1].ID, window.StationToAdd.ID);
                        innerWindow.ShowDialog();
                        if (!innerWindow.validClosed)
                            return;                        
                    }
                    if (index != path.Count() && !bl.getTwoFollowingStations(window.StationToAdd.ID, path[index].ID))
                    {
                        DistanceBetweenStations innerWindow = new DistanceBetweenStations(window.StationToAdd.ID, path[index].ID);
                        innerWindow.ShowDialog();
                        if (!innerWindow.validClosed)
                            return;
                    }

                    path.Insert(index, window.StationToAdd);
                    Ok.IsEnabled = OkButton_IsEnabled();
                }
            }
            catch (StationException ex) { MessageBox.Show(ex.Message); }
        }

        private void RemoveStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {              
                RemoveLineStation window = new RemoveLineStation(path);
                window.ShowDialog();
                int index = path.IndexOf(window.StationToRemove);
                if (index != -1) // selected
                {
                    if (index != 0 && index != path.Count() - 1 && !bl.getTwoFollowingStations(path[index - 1].ID, path[index + 1].ID))
                    {
                        DistanceBetweenStations innerWindow = new DistanceBetweenStations(path[index - 1].ID, path[index + 1].ID);
                        innerWindow.ShowDialog();
                        if (!innerWindow.validClosed)
                            return;
                    }

                    path.Remove(window.StationToRemove);
                    Ok.IsEnabled = OkButton_IsEnabled();
                }
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
