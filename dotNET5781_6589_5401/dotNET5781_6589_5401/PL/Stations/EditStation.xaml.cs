using BLAPI;
using BO;
using PO;
using System;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for EditStation.xaml
    /// Edit station's name or location
    /// If the location is changed, open another window (DistanceToOldStation) to get the length
    /// </summary>
    public partial class EditStation : Window
    {
        static IBL bl;

        public EditStation(Station station)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            DataContext = station;
        }       

        /// <summary>
        /// update the station
        /// if the argument are invalid throw an exception
        /// if the location has been change, open another window to get the distance between these two locations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {       
                double latitude = Convert.ToDouble(LatitudeTextBox.Text);
                if (latitude < 31 || latitude > 33.3)
                    throw new InvalidInputException("Latitude should be between 31°N to 33.3°N.");

                double longitude = Convert.ToDouble(LongitudeTextBox.Text);
                if (longitude < 34.3 || longitude > 35.5)
                    throw new InvalidInputException("Longitude should be between 34.3°E to 35.5°E.");

                int distanceToPreviousLocation = 0;
                if (latitude != ((Station)DataContext).Latitude || longitude != ((Station)DataContext).Longitude)
                {
                    DistanceToOldStation window = new DistanceToOldStation();
                    window.ShowDialog();
                    if (!window.valid)
                        return; // break out if distance was not written
                    distanceToPreviousLocation = window.Distance;
                }

                bl.updateStation(new Station { ID = int.Parse(IDLabel.Content.ToString()), Name = NameTextBox.Text, Latitude = latitude, Longitude = longitude }, distanceToPreviousLocation);
                Close();
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid location format.");
            }
            catch (StationException ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
