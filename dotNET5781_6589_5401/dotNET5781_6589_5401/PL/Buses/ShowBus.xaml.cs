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
    /// Interaction logic for ShowBus.xaml
    /// </summary>
    public partial class ShowBus : Window
    {
        static IBL bl;
        /// <summary>
        /// constructor
        /// </summary>
        public ShowBus(BO.Bus selectedBus)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            Details.DataContext = selectedBus;

        }

        /// <summary>
        /// In order to knew which bus to show in the new window.
        /// </summary>
        /// <param name="selectedBus">The bus for show</param>


        /// <summary>
        /// Click on button for service about the bus.
        /// </summary>
        /// <param name="sender">Button that the bus contect to it.</param>
        /// <param name="e"></param>
        private void TreatButton_Click(object sender, RoutedEventArgs e)
        {
            Button treat = (Button)sender;
            if (treat.DataContext is BO.Bus)
            {
                BO.Bus bus = (BO.Bus)treat.DataContext;
                
            }
        }

        /// <summary>
        /// Click on button for fuel bus. summon the function that do it.
        /// </summary>
        /// <param name="sender">Button that binding to bus in the list</param>
        /// <param name="e"></param>
        private void FuelButton_Click(object sender, RoutedEventArgs e)
        {
            Button fueling = (Button)sender;
            if (fueling.DataContext is BO.Bus)
            {
                BO.Bus bus = (BO.Bus)fueling.DataContext;
                
            }
        }

        /// <summary>
        /// click indicate O.K and close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

