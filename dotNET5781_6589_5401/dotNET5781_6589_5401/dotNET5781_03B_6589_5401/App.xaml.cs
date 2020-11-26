using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace dotNET5781_03B_6589_5401
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            DriveBus window = new DriveBus();
            window.ShowDialog();
            
        }

        private void FuelButton_Click(object sender, RoutedEventArgs e)
        {
            
        }


    }
}
