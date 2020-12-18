using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using BLAPI;
using PO;
using BO;
using System.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for Buses.xaml
    /// </summary>
    public partial class Buses : Window
    {
        public BackgroundWorker worker;

        static IBL bl;

        public Buses()
        {
            InitializeComponent();
            bl = BlFactory.GetBl();

            BusesList.ItemsSource = bl.GetBuses();
            RemoveBusButton.DataContext = bl.GetBuses();
            NumberOfBuses.DataContext = bl.GetBuses();

            worker = new BackgroundWorker();
            worker.DoWork += startTimer;
            worker.ProgressChanged += showTimer;
            worker.RunWorkerCompleted += updateBusProperties;
            worker.WorkerReportsProgress = true;
        }
        /// <summary>
        /// Do work - drive / fuel / service (=treat)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">list of 3 element: 
        /// 1. time to run 
        /// 2. bus 
        /// 3. km to update or negative number for indicate if this fuel or treat</param>
        private void startTimer(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            e.Result = e.Argument;

            Bus bus = (Bus)((List<object>)e.Argument)[1];
            int time = (int)((List<object>)e.Argument).First();

            bus.Time = $"{time / 6:00}:{time % 6:00}:00"; // *10/60=/6

            for (int i = time - 1; i > 0; i--)
            {
                Thread.Sleep(1000);
                worker.ReportProgress(i, e.Argument);
            }

            Thread.Sleep(900); // Split the last second in order to the user could see the time: "00:00:00" 
            worker.ReportProgress(0, e.Argument);
            Thread.Sleep(100);
        }

        /// <summary>
        /// Change the time remianed for the runing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">list of 3 element: 
        /// 1.time to run 
        /// 2.bus 
        /// 3.km to update or negative number for indicate if this fuel or treat
        /// and int for report</param>
        private void showTimer(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage * 10; // 1 unreal second = 10 real minutes     
            BO.Bus bus = (BO.Bus)((List<object>)e.UserState)[1];
            bus.Time = $"{progress / 60:00}:{progress % 60:00}:00";
        }

        /// <summary>
        /// In the end of the thread. Update fields of the bus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">list of 3 element: 
        /// 1.time to run 
        /// 2.bus 
        /// 3.km to update or negative number for indicate if this fuel or treat</param>
        private void updateBusProperties(object sender, RunWorkerCompletedEventArgs e)
        {
            BO.Bus bus = (BO.Bus)((List<object>)e.Result)[1];

            float km = (float)((List<object>)e.Result)[2];

            else if (km == -1) // means refueling
                bus.KmSinceFueled = 0;
            else // means service
            {
                bus.KmSinceTreated = 0;
                bus.DateOfLastTreat = DateTime.Now.Date;

                if (bus.KmSinceFueled >= 1200) // refuel if needed
                    bus.KmSinceFueled = 0;
            }

            bus.Status = bus.setState();
            bus.setCanBeFueled();
            bus.setCanBeServiced();
            bus.Time = "";
        }

        /// <summary>
        /// Double click on a bus in the list.
        /// </summary>
        /// <param name="sender">The bus that selected.</param>
        /// <param name="e"></param>
        private void BusesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Bus selectedBus = (Bus)BusesList.SelectedItem;
            ShowBusDetails window = new ShowBusDetails();
            window.update(selectedBus);
            window.ShowDialog();
        }


        /// <summary>
        /// Button for fuel bus. summon the function that do it.
        /// </summary>
        /// <param name="sender">Button that binding to bus in the list</param>
        /// <param name="e"></param>
        private void FuelButton_Click(object sender, RoutedEventArgs e)
        {
            Button fueling = (Button)sender;
            if (fueling.DataContext is Bus)
            {
                Bus bus = (Bus)fueling.DataContext;
                bus.fuel();
            }
        }

        /// <summary>
        /// Button for add bus. Open new window for insert data about the new bus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBusButton_Click(object sender, RoutedEventArgs e)
        {
            AddBus window = new AddBus();
            window.ShowDialog();
        }

        /// <summary>
        /// Button for remove bus. open new window that get id of bus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveBusButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveBus window = new RemoveBus();
            window.ShowDialog();
        }

        /// <summary>
        /// Button that close the progrem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            return;
        }
    }

    /// <summary>
    /// convert type state to type bool for binding to property with bool value.
    /// </summary>
    public class StateToBool_Drive : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((BO.State)value == BO.State.canDrive)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// convert type int to type bool for binding to property with bool value.
    /// </summary>
    public class intToBool_remove : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}