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

namespace temp
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window
    {
        bool administrativePrivileges;

        public NewAccount(bool a)
        {
            InitializeComponent();
            administrativePrivileges = a;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void ExistingUser_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SignInAsManager window = new SignInAsManager(administrativePrivileges);
            window.Show();
            Close();
        }
    }
}
