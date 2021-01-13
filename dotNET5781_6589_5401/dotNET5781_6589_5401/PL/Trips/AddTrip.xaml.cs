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
        private int line;

        public AddTrip(int lineNumber)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();

            line = lineNumber;
            Title = "Add Trip - Line " + lineNumber;

            StartHours.ItemsSource = Enumerable.Range(6, 18);
            StartHours.SelectedIndex = 0;
            EndHours.ItemsSource = Enumerable.Range(6, 18);
            EndHours.SelectedIndex = 0;
            StartMinutes.ItemsSource = Enumerable.Range(0, 60);
            StartMinutes.SelectedIndex = 0;
            EndMinutes.ItemsSource = Enumerable.Range(0, 60);
            EndMinutes.SelectedIndex = 0;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan start = new TimeSpan((int)StartHours.SelectedItem, (int)StartMinutes.SelectedItem, 0);
                TimeSpan end = new TimeSpan((int)EndHours.SelectedItem, (int)EndMinutes.SelectedItem, 0);
                int frequency = Convert.ToInt32(Frequency.Text);

                if (start > end)
                    throw new InvalidInputException("The start time should be before the end time.");

                if(start + frequency.MinutesToTimeSpan() > end)
                    throw new InvalidInputException("In order to schedule one trip, choose zero as a frequency.");

                bl.addDrivingLine(new DrivingLine() { NumberLine = line, Start = start, End = end, Frequency = frequency });
                Close();
            }
            catch (InvalidInputException ex) { MessageBox.Show(ex.Message); }
            catch (FormatException ex) { MessageBox.Show(ex.Message); }
            catch (TripException ex) { MessageBox.Show(ex.Message); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
      
        private void Ok_IsEnabled(object sender, TextChangedEventArgs e)
        {
            if (Frequency.Text.Length > 0)
                Ok.IsEnabled = true;
            else
                Ok.IsEnabled = false;
        }      
    }
}
