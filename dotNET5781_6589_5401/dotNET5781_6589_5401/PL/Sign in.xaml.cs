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

namespace PL
{
    /// <summary>
    /// Interaction logic for Sign_in.xaml
    /// </summary>
    public partial class Sign_in : Window
    {
        bool administrativePrivileges;

        public Sign_in(bool a)
        {
            InitializeComponent();
            administrativePrivileges = a;
        }

        private void NewAccount_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new New_account(administrativePrivileges).Show();
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Users.signIn(Username.Text, Password.Password, administrativePrivileges))
                    throw new Exception("Incorrect username or password.");

                if (administrativePrivileges)
                {
                    ManagerWindow window = new ManagerWindow(Username.Text);
                    window.Show();
                    Close();
                }
                //else
                //{
                //    PassengerWindow window = new PassengerWindow(Username.Text);
                //    window.Show();
                //    Close();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            new Main().Show();
            Close();
        }
    }
}
