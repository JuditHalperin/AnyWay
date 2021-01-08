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
using PO;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DistanceToOldStation.xaml
    /// </summary>
    public partial class DistanceToOldStation : Window
    {
        public int Distance = 0;
        public bool valid = false;

        public DistanceToOldStation()
        {
            InitializeComponent();
            Massage.Content = "You have changed the location of the station. \n" +
                "Enter the distance to the old location: \n" +
                "(use + or - to for direction)";
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(DistanceTextBox.Text, out Distance))
                    throw new InvalidInputException("Worng format of distance.");
                if (Distance == 0 || Distance < -240000 || Distance > 240000) // outside of Israel
                    throw new InvalidInputException("Invalid length.");
                valid = true;
                Close();
            }
            catch(InvalidInputException ex)
            { 
                MessageBox.Show(ex.Message);
            }
        }
    }
}
