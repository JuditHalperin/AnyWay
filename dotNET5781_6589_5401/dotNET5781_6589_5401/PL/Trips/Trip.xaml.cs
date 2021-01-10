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

namespace PL
{
    /// <summary>
    /// Interaction logic for Trip.xaml
    /// </summary>
    public partial class Trip : Window
    {
        static IBL bl;

        string username;

        public Trip(string name)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            username = name;

            IEnumerable<Station> stations = bl.GetStations();
            SourseStation.ItemsSource = stations;
            TargetStation.ItemsSource = stations;
        }

        private void visibilities(object sender, SelectionChangedEventArgs e)
        {
            if (SourseStation.SelectedItem != null && TargetStation.SelectedItem != null)
                if (((Station)SourseStation.SelectedItem).ID == ((Station)TargetStation.SelectedItem).ID)
                {
                    SameStation.Visibility = Visibility.Visible;
                    NoLines.Visibility = Visibility.Hidden;
                    Titles.Visibility = Visibility.Hidden;
                    Trips.Visibility = Visibility.Hidden;
                }
                else
                {
                    SameStation.Visibility = Visibility.Hidden;

                    var trips = from DrivingBus drivingBus in bl.getPassengerTrips(((Station)SourseStation.SelectedItem).ID, ((Station)TargetStation.SelectedItem).ID)
                                let timeOfJourney = bl.durationTripBetweenStations(drivingBus.NumberLine, ((Station)SourseStation.SelectedItem).ID, ((Station)TargetStation.SelectedItem).ID)
                                let totalTime = bl.timeToTargetStation(drivingBus, ((Station)TargetStation.SelectedItem).ID)                     
                                select new // anonymous variable for PL trip
                                {
                                    Line = drivingBus.NumberLine,
                                    TimeTillArrival = (totalTime - timeOfJourney).ToString(@"hh\:mm\:ss"),
                                    TimeOfJourney = timeOfJourney,
                                    TotalTime = totalTime.ToString(@"hh\:mm\:ss")
                                };

                    if (trips.Count() == 0)
                    {
                        NoLines.Visibility = Visibility.Visible;
                        Titles.Visibility = Visibility.Hidden;
                        Trips.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        Trips.ItemsSource = trips.OrderBy(item => item.TotalTime);
                        NoLines.Visibility = Visibility.Hidden;
                        Titles.Visibility = Visibility.Visible;
                        Trips.Visibility = Visibility.Visible;                        
                    }                    
                }
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new PassengerWindow(username).Show();
            Close();
        }
    }
}