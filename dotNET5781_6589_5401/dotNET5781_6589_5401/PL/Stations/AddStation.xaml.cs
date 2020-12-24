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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        static IBL bl;

        public AddStation()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            Ok.IsEnabled = false; // enabled when all TextBox are full
        }

        /// <summary>
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (!int.TryParse(IDTextBox.Text, out id) || IDTextBox.Text.Length < 4)
                    throw new InvalidInputException("Station ID should be consisted of 4 digits.");
                bl.addStation(new Station() { ID = id, Name = NameTextBox.Text, Latitude = Convert.ToDouble(LatitudeTextBox.Text), Longitude = Convert.ToDouble(LongitudeTextBox.Text) });
                Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid format of location.");
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
