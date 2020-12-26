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

namespace PL.Lines
{
    /// <summary>
    /// Interaction logic for LinesList.xaml
    /// </summary>
    public partial class LinesList : Window
    {
        static IBL bl;

        string username;

        public LinesList(string name)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            username = name;

            IEnumerable<BO.Line> lines = bl.GetLines();
            ListOfLines.ItemsSource = lines;
            if(lines.Count() > 0)
            {
                ListOfLines.SelectedIndex = 0;
                ListOfLines.Text = ((BO.Line)ListOfLines.SelectedItem).ThisSerial.ToString();
                selectionChanged();
            }          
        }

        private void selectionChanged()
        {
            DataContext = (BO.Line)ListOfLines.SelectedItem;
            int numberOfStations = ((BO.Line)DataContext).Path.Count();
            NumberOfStations.Content = numberOfStations;
            if (numberOfStations > 0)
            {
                NoStations.Visibility = Visibility.Hidden;
                LineStations.Visibility = Visibility.Visible;
            }
            else
            {
                LineStations.Visibility = Visibility.Hidden;
                NoStations.Visibility = Visibility.Visible;
            }
        }

        private void ListOfLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectionChanged();
        }
        
        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            new AddLine().ShowDialog();
        }

        private void EditLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!bl.canChangeLine((BO.Line)ListOfLines.SelectedItem))
                    throw new LineException("Impossible to edit a line if it is driving.");
                new EditLine((BO.Line)ListOfLines.SelectedItem).ShowDialog();
            }
            catch (LineException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!bl.canChangeLine((BO.Line)ListOfLines.SelectedItem))
                    throw new LineException("Impossible to remove a line if it is driving.");
                bl.removeLine((BO.Line)ListOfLines.SelectedItem);
            }
            catch (LineException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LineStations_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new LineStationDetails((LineStation)LineStations.SelectedItem).ShowDialog();
        }     

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new ManagerWindow(username).Show();
            Close();
        }
    }
}
