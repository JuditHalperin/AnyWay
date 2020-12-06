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

namespace dotNET5781_03B_6589_5401
{
    /// <summary>
    /// Interaction logic for RemoveBus.xaml
    /// </summary>
    public partial class RemoveBus : Window
    {
        /// <summary>
        /// constructor
        /// </summary>
        public RemoveBus()
        {
            InitializeComponent();
        }

        /// <summary>
        /// event: clicking 'Ok' button
        /// remove the bus with the given license plate number, or throw an exception
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string value = LicensePlate.Text;

                if (value.Length < 7 || value.Length > 8) // wrong length
                    throw new BasicBusExceptions("Wrong length of license plate number.");

                int num = int.Parse(value);

                if (value.Length == 7) // 7 digits
                {
                    value = value.Insert(2, "-");
                    value = value.Insert(6, "-");
                }

                else if (value.Length == 8) // 8 digits
                {
                    value = value.Insert(3, "-");
                    value = value.Insert(6, "-");
                }

                Close();

                Buses.removeBus(value);
            }
            catch (BasicBusExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid license plate number.");
            }
        }

        /// <summary>
        /// event: clicking 'Cancel' button
        /// close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            LicensePlate.Text = "";
            Close();
        }

        /// <summary>
        /// event: after each character is typed, read it only if it is a digit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LicensePlate_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            // alloe Enter and Tab keys
            if (e.Key == Key.Return || e.Key == Key.Enter || e.Key == Key.Tab)
                return;
          
            // allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
           || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            // allow control system keys
            if (Char.IsControl(c)) return;

            // allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; // let this key be written inside the textbox

            // forbid letters and signs (#,$, %, ...)
            e.Handled = true; // ignore this key. mark event as handled, will not be routed to other controls
            return;
        }        
    }
}
