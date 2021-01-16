using PO;
using System.Windows;
using System.Windows.Input;

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
