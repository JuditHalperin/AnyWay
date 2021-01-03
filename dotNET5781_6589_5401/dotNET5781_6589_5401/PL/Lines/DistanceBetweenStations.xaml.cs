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

namespace PL
{
    /// <summary>
    /// Interaction logic for DistanceBetweenStations.xaml
    /// </summary>
    public partial class DistanceBetweenStations : Window
    {
        static IBL bl;
        public bool validClosed = false;
        public bool canceled = false;
        int firstStationID;
        int secondStationID;

        public DistanceBetweenStations(int previousStationID, int thisStationID)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            firstStationID = previousStationID;
            secondStationID = thisStationID;
            Massage.Content = $"Enter the distance between station {firstStationID} and station {secondStationID}:";
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int distance;
                if (!int.TryParse(DistanceTextBox.Text, out distance))
                    throw new InvalidInputException("Invalid format of distance.");
                if (distance <= 0 || distance > 240000) // outside of Israel
                    throw new InvalidInputException("Invalid distance.");
                bl.addTwoFollowingStation(firstStationID, secondStationID, distance);
                validClosed = true;
                Close();
            }
            catch (InvalidInputException ex) { MessageBox.Show(ex.Message); }
            catch (StationException ex) { MessageBox.Show(ex.Message); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            validClosed = true;
            canceled = true;
            Close();
        }
    }
}
