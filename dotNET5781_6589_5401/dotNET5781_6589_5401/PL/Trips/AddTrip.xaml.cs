using BLAPI;
using BO;
using PO;
using System;
using System.Windows;

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

            for (int i = 6; i < 24; i++)
            {
                StartHours.Items.Add(i.ToString("00"));
                EndHours.Items.Add(i.ToString("00"));
            }
            StartHours.SelectedIndex = 0;
            EndHours.SelectedIndex = 0;

            for (int i = 0; i < 60; i++)
            {
                StartMinutes.Items.Add(i.ToString("00"));
                EndMinutes.Items.Add(i.ToString("00"));
            }
            StartMinutes.SelectedIndex = 0;
            EndMinutes.SelectedIndex = 0;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan start = new TimeSpan(Convert.ToInt32(StartHours.SelectedItem), Convert.ToInt32(StartMinutes.SelectedItem), 0);
                TimeSpan end = new TimeSpan(Convert.ToInt32(EndHours.SelectedItem), Convert.ToInt32(EndMinutes.SelectedItem), 0);
                int frequency = Convert.ToInt32(Frequency.Text);

                if (start > end)
                    throw new InvalidInputException("The start time should be before the end time.");

                if (start + frequency.MinutesToTimeSpan() > end) // one trip
                {
                    frequency = 0;
                    end = start;
                }

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
    }
}
