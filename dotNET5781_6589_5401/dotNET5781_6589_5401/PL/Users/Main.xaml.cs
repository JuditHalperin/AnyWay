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
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            NotifyingDateTime dateTime = new NotifyingDateTime();
            Time.DataContext = dateTime;
            dateTime.worker.RunWorkerAsync();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void SignInAsManager_Click(object sender, RoutedEventArgs e)
        {
            new SignIn(true).Show();
            Close();
        }

        private void SignInAsPassenger_Click(object sender, RoutedEventArgs e)
        {
            new SignIn(false).Show();
            Close();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new About().ShowDialog();
        }
    }
}
