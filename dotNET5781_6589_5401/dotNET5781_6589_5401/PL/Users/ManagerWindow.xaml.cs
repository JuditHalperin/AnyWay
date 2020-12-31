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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        static IBL bl;

        public ManagerWindow(string username)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            Username.Content = username;

            if (bl.NoBuses()) AddFirstBus.IsEnabled = true;
            else ShowBuses.IsEnabled = true;
            if (bl.NoLines()) AddFirstLine.IsEnabled = true;
            else ShowLines.IsEnabled = true; 
            if (bl.NoStations()) AddFirstStation.IsEnabled = true;
            else ShowStations.IsEnabled = true;           
        }

        private void ShowBuses_Click(object sender, RoutedEventArgs e)
        {
            new BusesList((string)Username.Content).Show();
            Close();
        }

        private void ShowLines_Click(object sender, RoutedEventArgs e)
        {
            new LinesList((string)Username.Content).Show();
            Close();
        }

        private void ShowStations_Click(object sender, RoutedEventArgs e)
        {
            new StationsList((string)Username.Content).Show();
            Close();
        }
        
        private void AddFirstBus_Click(object sender, RoutedEventArgs e)
        {
            new AddBus().ShowDialog();
            new ManagerWindow((string)Username.Content).Show();
            Close();
        }

        private void AddFirstLine_Click(object sender, RoutedEventArgs e)
        {
            new AddLine().ShowDialog();
            new ManagerWindow((string)Username.Content).Show();
            Close();
        }

        private void AddFirstStation_Click(object sender, RoutedEventArgs e)
        {
            new AddStation().ShowDialog();
            new ManagerWindow((string)Username.Content).Show();
            Close();
        }

        private void SignOut_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new Main().Show();
            Close();
        }

        private void ChangePassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new ChangePassword("password", (string)Username.Content).ShowDialog();
        }

        private void ChangeCode_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new ChangePassword("managing code", (string)Username.Content).ShowDialog();
        }
    }
}
