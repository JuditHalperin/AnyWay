using BLAPI;
using BO;
using PO;
using System.Windows;
using System.Windows.Input;

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
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            NotifyingDateTime dateTime = new NotifyingDateTime();
            Time.DataContext = dateTime;
            dateTime.worker.RunWorkerAsync();

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
                if (user == null)
                    throw new InvalidInputException("The username not exist, you can to create new account.");
                if (user.Password != Password.Password || user.IsManager != administrativePrivileges)
                    throw new InvalidInputException("Incorrect password or administrative privileges.");
                if (administrativePrivileges)
                    new ManagerWindow(Username.Text).Show();
                else
                    new PassengerWindow(Username.Text).Show();
                    Close();
                Close();
            }
            catch (UserException ex) { MessageBox.Show(ex.Message); }           
            catch (InvalidInputException ex) { MessageBox.Show(ex.Message); }            
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
