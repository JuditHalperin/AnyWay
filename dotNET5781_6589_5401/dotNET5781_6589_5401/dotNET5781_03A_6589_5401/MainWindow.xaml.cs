/*
Judit Halperin - 324216589
Asnat Kahane - 211825401

Exercise 3A
15/11/20
This program allows the user to choose a bus line, and presents its area and stastions.
*/

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotNET5781_02_6589_5401;

namespace dotNET5781_03A_6589_5401
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusLine currentDisplayBusLine;
        private List<BusStation> stations;
        private BusesCollection buses;

        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
            stations = Program.initializeBusStations();
            buses = Program.initializeBusesCollection(stations);

            InitializeComponent();

            cbBusLines.ItemsSource = buses;
            cbBusLines.DisplayMemberPath = "Line";
            cbBusLines.SelectedIndex = 0;            
        }

        /// <summary>
        /// show stations and area of the given bus
        /// for each station: number, location, time since last station
        /// </summary>
        /// <param name="index">line</param>
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = buses[index];            
            UpGrid.DataContext = currentDisplayBusLine;
            IbBusLineStations.DataContext = currentDisplayBusLine.Path;
            tbArea.Text = $"{currentDisplayBusLine.Region}";
        }

        /// <summary>
        /// event: selection of line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).Line);
        }
    }
}
