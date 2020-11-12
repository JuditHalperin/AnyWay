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
        // icon
        // ShowBusLine - what to do at constructor?
        // Area - default

        private BusLine currentDisplayBusLine;
        private List<BusStation> stations;
        private BusesCollection buses;

        public MainWindow()
        {
            stations = Program.initializeBusStations();
            buses = Program.initializeBusesCollection(stations);

            InitializeComponent();

            cbBusLines.ItemsSource = buses;
            cbBusLines.DisplayMemberPath = "Line";
            cbBusLines.SelectedIndex = 0;
            
            //ShowBusLine();
        }

        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = buses[index];
            
            UpGrid.DataContext = currentDisplayBusLine;

            IbBusLineStations.DataContext = currentDisplayBusLine.Path;

            tbArea.Text = $"{currentDisplayBusLine.Region}";
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).Line);
        }
    }
}
