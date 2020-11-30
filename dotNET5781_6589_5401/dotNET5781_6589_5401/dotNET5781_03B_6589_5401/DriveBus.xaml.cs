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
    /// Interaction logic for DriveBus.xaml
    /// </summary>
    public partial class DriveBus : Window
    {
        public DriveBus()
        {
            InitializeComponent();
        }

        public void update(Bus selectedBus)
        {
            Drive.DataContext = selectedBus;
        }

        private void Length_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;

            // close the window when Enter is clicked
            if (e.Key == Key.Return || e.Key == Key.Enter || e.Key == Key.Tab)
            {
                try
                {
                    double km = Convert.ToDouble(Length.Text);
                    if (km <= 0)
                        throw new BasicBusExceptions("Negative distance of drive is invalid.");
                    
                    Close();
                    e.Handled = true;

                    (Drive.DataContext as Bus).drive((float)km);

                    return;
                }
                catch (BasicBusExceptions ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid distance of drive.");
                }                
            }

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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Length.Text = "";
            Close();
        }
    }
}
