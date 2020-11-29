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
    }
}
