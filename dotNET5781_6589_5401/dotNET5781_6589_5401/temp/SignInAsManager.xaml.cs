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
    /// Interaction logic for SignInAsManager.xaml
    /// </summary>
    public partial class SignInAsManager : Window
    {
        bool administrativePrivileges;

        public SignInAsManager(bool a)
        {
            InitializeComponent();
            administrativePrivileges = a;
        }

        private void NewAccount_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NewAccount window = new NewAccount(administrativePrivileges);
            window.Show();
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                if (!Users.signIn(Username.Text, Password.Password, administrativePrivileges))
                    throw new Exception("Incorrect username or password.");

                // new window
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }       
    }
}
