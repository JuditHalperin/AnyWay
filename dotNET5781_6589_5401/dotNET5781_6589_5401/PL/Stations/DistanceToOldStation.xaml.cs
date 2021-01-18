using PO;
using System.Windows;

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
                "(use plus or minus for direction)";
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(DistanceTextBox.Text, out Distance))
                    throw new InvalidInputException("Invalid format of distance.");
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
