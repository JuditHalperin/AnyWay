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
            worker.RunWorkerCompleted += updateBusProperties;
            worker.WorkerReportsProgress = true;
        }

        private void startTimer(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            e.Result = e.Argument;

            Bus bus = (Bus)((List<object>)e.Argument)[1];

            bus.Time = "00:00:00";

            for (int i = 1; i < (int)((List<object>)e.Argument).First(); i++)
            {
                Thread.Sleep(1000);
                worker.ReportProgress(i, e.Argument);
            }
        }

        private void showTimer(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage * 10; // 1 unreal second = 10 real minutes     
            Bus bus = (Bus)((List<object>)e.UserState)[1];
            bus.Time = $"{progress / 60: 00}:{progress % 60: 00}:00";
        }

        private void updateBusProperties(object sender, RunWorkerCompletedEventArgs e)
        {
            Bus bus = (Bus)((List<object>)e.Result)[1];

            float km = (float)((List<object>)e.Result)[2];

            if (km > 0) // drive the give distance
                bus.updateKm(km);
            else if (km == -1) // means refueling
                bus.KmSinceFueled = 0;
            else // means service
            {
                bus.KmSinceTreated = 0;
                bus.DateOfLastTreat = DateTime.Now.Date;

                if (bus.KmSinceFueled >= 1200)
                    bus.KmSinceFueled = 0;
            }

            bus.Status = bus.setState();
            bus.setCanBeFueled();
            bus.setCanBeServiced();
            bus.Time = "";
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
            RemoveBus window = new RemoveBus();
            window.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class StateToBool_Drive : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            State stateValue = (State)value;
            if (stateValue == State.canDrive)
                return true;

            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusToText_Status : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            State stateValue = (State)value;

            switch (stateValue)
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
