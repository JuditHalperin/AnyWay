using BLAPI;
using BO;
using PO;
using System;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        static IBL bl;

        public AddStation()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// add a new station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id;
                if (!int.TryParse(IDTextBox.Text, out id) || id < 10000 || id > 99999)
                    throw new InvalidInputException("Station ID should be consisted of 5 digits.");

                double latitude = Convert.ToDouble(LatitudeTextBox.Text);
                if (latitude < 31 || latitude > 33.3)
                    throw new InvalidInputException("Latitude should be between 31°N to 33.3°N.");

                double longitude = Convert.ToDouble(LongitudeTextBox.Text);
                if (longitude < 34.3 || longitude > 35.5)
                    throw new InvalidInputException("Longitude should be between 34.3°E to 35.5°E.");

                bl.addStation(new Station() { ID = id, Name = NameTextBox.Text, Latitude = latitude, Longitude = longitude });
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid location format.");
            }
            catch (StationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
