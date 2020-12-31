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
using BO;
using PO;

namespace PL
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        static IBL bl;

        bool administrativePrivileges;

        public SignIn(bool a)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            administrativePrivileges = a;
        }

        private void NewAccount_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new NewAccount(administrativePrivileges).Show();
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = bl.getUser(Username.Text);
                if (user.Password != Password.Password || user.IsManager != administrativePrivileges)
                    throw new InvalidInputException("Incorrect password or administrative privileges.");
                if (administrativePrivileges)
                {
                    new ManagerWindow(Username.Text).Show();
                    Close();
                }
            }
            catch (UserException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            new Main().Show();
            Close();
        }       

        /// <summary>
        /// enable to press 'Ok' when all fields are full
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ok_IsEnabled(object sender, RoutedEventArgs e)
        {
            if (Username.Text.Length > 0 && Password.Password.Length > 0)
                Ok.IsEnabled = true;
            else
                Ok.IsEnabled = false;
        }       
    }
}
