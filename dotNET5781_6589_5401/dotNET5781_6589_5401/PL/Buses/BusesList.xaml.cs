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
using System.Threading;
using BLAPI;
using PO;
using BO;

namespace PL.Buses
{
    /// <summary>
    /// Interaction logic for BusesList.xaml
    /// </summary>
    public partial class BusesList : Window
    {
        public BackgroundWorker worker;

        //static IBL bl;

        public BusesList(string username)
        {
            InitializeComponent();
            //bl = BlFactory.GetBl();
            //ManagerWindow managerWindow = new ManagerWindow(username); // open when 'cancel' is clicked

            //BusesList.ItemsSource = bl.GetBuses();
            //RemoveBusButton.DataContext = bl.GetBuses();
            //NumberOfBuses.DataContext = bl.GetBuses();

            //worker = new BackgroundWorker();
            //worker.DoWork += startTimer;
            //worker.ProgressChanged += showTimer;
            //worker.RunWorkerCompleted += updateBusProperties;
            //worker.WorkerReportsProgress = true;
        }

    }
}
