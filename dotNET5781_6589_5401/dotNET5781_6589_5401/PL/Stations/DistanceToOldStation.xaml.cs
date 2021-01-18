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
        public bool valid = false; // test if the window was closed by the 'ok' button - which means distance was written

        public DistanceToOldStation()
        {
            InitializeComponent();
            Massage.Content = "You have changed the location of the station. \n" +
                "Enter the distance (meters) to the old location: \n" +
                "(use plus or minus for direction)";
        }

        /// <summary>
        /// if the argument are invalid throw an exception
        /// otherwise, close the window and mark it as a valid close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
