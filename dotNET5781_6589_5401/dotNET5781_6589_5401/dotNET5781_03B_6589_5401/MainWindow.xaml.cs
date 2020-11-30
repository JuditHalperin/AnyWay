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
        public BackgroundWorker worker;

        public MainWindow()
        {
            InitializeComponent();

            BusesList.ItemsSource = Buses.buses;

            worker = new BackgroundWorker();

            worker.DoWork += startTimer;
            worker.ProgressChanged += showTimer;
            worker.WorkerReportsProgress = true;
        }


        private void startTimer(object sender, DoWorkEventArgs e)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            BackgroundWorker worker = sender as BackgroundWorker;

            for (int i = 1; i < (int)e.Argument; i++) 
            {
                Thread.Sleep(1000);
                worker.ReportProgress(i);
            }
        }

        private void showTimer(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage * 12;
            
            //TimerLabel.content = $"{progress / 60 : 00}:{progress % 60 : 00}:00";
            // ליצור תווית עם נראות
        }
        
        private void BusesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Bus selectedBus = (Bus)BusesList.SelectedItem;
            ShowBusDetails window = new ShowBusDetails();
            window.update(selectedBus);
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

        private void AddBusButton_Click(object sender, RoutedEventArgs e)
        {
            AddBus window = new AddBus();
            window.ShowDialog();
        }

        private void RemoveBusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

    public class StatusToText_Status : IValueConverter
    {
        public object Convert(
          object value,
          Type targetType,
          object parameter,
          CultureInfo culture)
        {
            State stateValue = (State)value;

            switch(stateValue)
            {
                case State.canDrive:
                    return "Can Drive";
                case State.cannotDrive:
                    return "Cannot Drive";
                case State.gettingFueled:
                    return "Being fueled";
                case State.gettingTreated:
                    return "Being serviced";
                case State.driving:
                    return "Driving";
                default:
                    return null;
            }
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
