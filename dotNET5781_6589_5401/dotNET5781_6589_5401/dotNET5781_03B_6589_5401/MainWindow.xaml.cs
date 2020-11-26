using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNET5781_03B_6589_5401
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BusesList.ItemsSource = Buses.buses;
        }

        private void AddBusButton_Click(object sender, RoutedEventArgs e)
        {
            AddBus window = new AddBus();
            window.ShowDialog();
        }

        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            DriveBus window = new DriveBus();
            window.ShowDialog();
        }

        private void FuelButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BusesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
    public class StateToBool_Drive : IValueConverter
    {
        public object Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            State stateValue = (State)value;
            if (stateValue == State.canDrive)
                return true;

            else
                return false;
        }

        public object ConvertBack(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FloatToBool_Fuel : IValueConverter
    {
        public object Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            float floatValue = (float)value;
            if (floatValue > 800)
                return true;

            else
                return false;
        }

        public object ConvertBack(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
