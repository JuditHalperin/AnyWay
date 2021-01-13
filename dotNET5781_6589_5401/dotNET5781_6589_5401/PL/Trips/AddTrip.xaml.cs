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
    /// Interaction logic for AddTrip.xaml
    /// </summary>
    public partial class AddTrip : Window
    {
        static IBL bl;

        public AddTrip(int line)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();

            Line.Content = $"Line {line}";
        }        

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan start = new TimeSpan();
                TimeSpan end = new TimeSpan();
                int frequency = Convert.ToInt32(Frequency.Text);

                if (start > end)
                    throw new InvalidInputException("Start time should be before end time.");

                if(start + frequency.MinutesToTimeSpan() > end)
                    throw new InvalidInputException("In order to schedule one trip choose a zero as a frequency.");

            }
            catch (InvalidInputException ex) { MessageBox.Show(ex.Message); }
            catch (FormatException ex) { MessageBox.Show(ex.Message); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ok_IsEnabled(object sender, TextChangedEventArgs e)
        {
            if (Frequency.Text.Length > 0 && true) // add the date
                Ok.IsEnabled = true;
            else
                Ok.IsEnabled = false;
        }
    }
}
