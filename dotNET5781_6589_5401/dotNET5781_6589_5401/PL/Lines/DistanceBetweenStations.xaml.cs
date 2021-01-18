using BLAPI;
using BO;
using PO;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for DistanceBetweenStations.xaml
    /// Get distance between two line station
    /// Used when add / edit a line and two following stations are added to the path, but there is no 'TwoFollowingStations' object for them
    /// </summary>
    public partial class DistanceBetweenStations : Window
    {
        static IBL bl;
        public bool validClosed = false; // test if the window was closed by the 'ok' button - which means distance was written
        int firstStationID;
        int secondStationID;

        public DistanceBetweenStations(int previousStationID, int thisStationID)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            firstStationID = previousStationID;
            secondStationID = thisStationID;
            Massage.Content = $"Enter the distance between station {firstStationID} and station {secondStationID}: (meters)";
        }

        /// <summary>
        /// create two following stations
        /// if the argument are invalid throw an exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int distance;
                if (!int.TryParse(DistanceTextBox.Text, out distance))
                    throw new InvalidInputException("Invalid format of distance.");
                if (distance <= 0 || distance > 240000) // outside of Israel
                    throw new InvalidInputException("Invalid distance.");
                
                bl.addTwoFollowingStations(firstStationID, secondStationID, distance);
                validClosed = true;
                Close();
            }
            catch (InvalidInputException ex) { MessageBox.Show(ex.Message); }
            catch (StationException ex) { MessageBox.Show(ex.Message); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
