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

namespace PL.Buses
{
    /// <summary>
    /// Interaction logic for AddBus.xaml
    /// </summary>
    public partial class AddBus : Window
    {
        //static IBL bl;
        /// <summary>
        /// constructor
        /// </summary>
        public AddBus()
        {
            InitializeComponent();
            //bl = BlFactory.GetBl();
            //BeginingDate.DisplayDateEnd = DateTime.Now.Date;
            //BeginingDate.SelectedDate = DateTime.Now.Date;
        }

        /// <summary>
        /// event: clicking 'Ok' button
        /// add a new bus and close the window, or throw exceptions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //    private void OkButton_Click(object sender, RoutedEventArgs e)
        //    {
        //        DateTime date = Convert.ToDateTime(BeginingDate.SelectedDate);

        //        try
        //        {
        //            bl.addBus(new Bus
        //            {
        //                LicensePlate = ID.Text,
        //                StartOfWork = date.Date,
        //                TotalKms = Convert.ToInt32(TotalKm.Text),
        //                LastService = date.Date
        //            });
        //            Close();
        //        }

        //        catch (BusException ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //        catch (FormatException)
        //        {
        //            MessageBox.Show("Invalid format.");
        //        }
        //    }

        //    /// <summary>
        //    /// event: clicking 'Cancel' button
        //    /// close the window
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    private void CancelButton_Click(object sender, RoutedEventArgs e)
        //    {
        //        Close();
        //    }

        //    /// <summary>
        //    /// event: after each character is typed, read it only if it is a digit
        //    /// </summary>
        //    /// <param name="sender"></param>
        //    /// <param name="e"></param>
        //    private void OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        //    {
        //        TextBox text = sender as TextBox;
        //        if (text == null) return;
        //        if (e == null) return;

        //        // alloe Enter and Tab keys
        //        if (e.Key == Key.Return || e.Key == Key.Enter || e.Key == Key.Tab)
        //            return;

        //        // allow list of system keys (add other key here if you want to allow)
        //        if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
        //        e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
        //       || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
        //            return;

        //        char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

        //        // allow control system keys
        //        if (Char.IsControl(c)) return;

        //        // allow digits (without Shift or Alt)
        //        if (Char.IsDigit(c))
        //            if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
        //                return; // let this key be written inside the textbox

        //        // forbid letters and signs (#,$, %, ...)
        //        e.Handled = true; // ignore this key. mark event as handled, will not be routed to other controls
        //        return;
        //    }
        //}
    }
}
