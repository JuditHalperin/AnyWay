﻿using System;
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

        public StationsList(string username)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            ManagerWindow managerWindow = new ManagerWindow(username); // open when 'cancel' is clicked

            ListOfStations.ItemsSource = bl.GetStations();
            ListOfStations.SelectedIndex = 0;
            selectionChanged();            
        }

        private void selectionChanged()
        {
            DataContext = (Station)ListOfStations.SelectedItem;
            IEnumerable<LineStation> lineStations = ((Station)ListOfStations.SelectedItem).LinesAtStation;
            if (lineStations.Count() > 0)
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

        private void StationsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged();
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            new AddStation().ShowDialog();
        }

        private void EditStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!bl.canChangeStation((Station)ListOfStations.SelectedItem))
                    throw new StationException("Impossible to update a station if there are driving lines that stop there.");
                new EditStation((Station)ListOfStations.SelectedItem).ShowDialog();
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
            }
            
            catch (StationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
