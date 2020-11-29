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
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;

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

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += startTimer;
            worker.ProgressChanged += showTimer;
        }
       
        private void startTimer(object sender, DoWorkEventArgs e)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 0; i < (int)e.Argument; i++) 
            {
                worker.ReportProgress((int)timer.ElapsedMilliseconds / 1000);
                Thread.Sleep(1000);
            }

        }

        private void showTimer(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage * 12;
            ProgressLabel.content = $"{progress / 60}:{progress % 60}:00";
            // ליצור תווית עם נראות ולסדר פורמט הדפסה
        }
       
        private void AddBusButton_Click(object sender, RoutedEventArgs e)
        {
            AddBus window = new AddBus();
            window.ShowDialog();
        }

        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            Button driving = (Button)sender;
            DriveBus window = new DriveBus();
            window.update((Bus)driving.DataContext);
            window.ShowDialog();            
        }

        private void FuelButton_Click(object sender, RoutedEventArgs e)
        {
            Button fueling = (Button)sender;
            if (fueling.DataContext is Bus)
            {
                Bus bus = (Bus)fueling.DataContext;
                bus.fuel();
            }
        }

        private void BusesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Bus selectedBus = (Bus)BusesList.SelectedItem;
            ShowBusDetails window = new ShowBusDetails();
            window.update(selectedBus);
            window.ShowDialog();
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

}
