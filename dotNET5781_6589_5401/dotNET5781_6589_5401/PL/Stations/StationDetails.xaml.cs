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
using BLAPI;
using PO;
using BO;

namespace PL.Stations
{
    /// <summary>
    /// Interaction logic for StationDetails.xaml
    /// </summary>
    public partial class StationDetails : Window
    {
        static IBL bl;

        public StationDetails(Station selectedStation)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            DataContext = selectedStation;
            LinesAtStation.ItemsSource = bl.GetLines(item =>
            {
                foreach (LineStation lineStation in item.Path)
                    if (lineStation.ID == selectedStation.ID)
                        return true;
                return false;

            });
        }


    }
}
