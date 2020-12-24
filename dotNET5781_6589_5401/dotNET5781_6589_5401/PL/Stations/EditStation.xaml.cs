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
                int id;
                if (!int.TryParse((string)IDLabel.Content, out id))
                    throw new InvalidInputException("Invalid format of station ID.");
                bl.updateStation(new Station { ID = id, Name = NameTextBox.Text, Latitude = Convert.ToDouble(LatitudeTextBox.Text), Longitude = Convert.ToDouble(LongitudeTextBox.Text) });
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
