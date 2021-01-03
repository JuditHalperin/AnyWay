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
    /// Interaction logic for EditStation.xaml
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

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
                    while (!window.valid)
                    {
                        window = new DistanceToOldStation();
                        window.ShowDialog();
                    }
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
    }
}
