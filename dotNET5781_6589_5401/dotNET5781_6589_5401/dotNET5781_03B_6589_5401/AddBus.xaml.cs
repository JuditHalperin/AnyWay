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
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        public AddBus()
        {
            InitializeComponent();

            BeginingDate.DisplayDateEnd = DateTime.Now.Date;
            BeginingDate.SelectedDate = DateTime.Now.Date;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = Convert.ToDateTime(BeginingDate.SelectedDate);
            
            try
            {
                Buses.addBus(new Bus(date.Date, date.Date, ID.Text, Convert.ToInt32(TotalKm.Text)));
                Close();
            }

            catch (BasicBusExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid format.");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
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
