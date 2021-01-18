using BLAPI;
using BO;
using PO;
using System;
using System.Windows;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// Create a new account
    /// </summary>
    public partial class NewAccount : Window
    {
        static IBL bl;

        bool administrativePrivileges;

        public NewAccount(bool a)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            NotifyingDateTime dateTime = new NotifyingDateTime();
            Time.DataContext = dateTime;
            dateTime.worker.RunWorkerAsync();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            administrativePrivileges = a;
            if (!a)
            {
                LabelOfManagingCode.Visibility = Visibility.Hidden;
                ManagingCode.Visibility = Visibility.Hidden;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (administrativePrivileges && bl.getManagingCode() != ManagingCode.Password)
                    throw new InvalidInputException("Incorrect managing code.");

                string message = validPassword(Password.Password);
                if (message != "Valid")
                    MessageBox.Show(message);

                else
                {
                    bl.addUser(new User() { Username = Username.Text, Password = Password.Password, IsManager = administrativePrivileges });
                    if (administrativePrivileges)
                        new ManagerWindow(Username.Text).Show();
                    else
                        new PassengerWindow(Username.Text).Show();
                    Close();
                    return;
                }
            }

            catch (InvalidInputException ex) { MessageBox.Show(ex.Message); }            
            catch (UserException ex) { MessageBox.Show(ex.Message); }            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            new Main().Show();
            Close();
        }

        private void ExistingUser_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new SignIn(administrativePrivileges).Show();
            Close();
        }

        private string validPassword(String password)
        {
            if (password.Length < 6)
                return "Password length should be between 6 to 8 characters.";

            if (password.Contains(" "))
                return "Password should not contain any space.";

            int count = 0;
            for (int i = 0; i <= 9; i++)
            {
                string str1 = i.ToString();
                if (password.Contains(str1))
                {
                    count = 1;
                    break;
                }
            }
            if (count == 0)
                return "Password should contain at least one digit.";


            if (!(password.Contains("@") || password.Contains("#")
                || password.Contains("!") || password.Contains("~")
            || password.Contains("$") || password.Contains("%")
            || password.Contains("^") || password.Contains("&")
            || password.Contains("*") || password.Contains("(")
            || password.Contains(")") || password.Contains("-")
            || password.Contains("+") || password.Contains("/")
            || password.Contains(":") || password.Contains(".")
            || password.Contains(", ") || password.Contains("<")
            || password.Contains(">") || password.Contains("?")
            || password.Contains("|")))
                return "Password should contain at least one special character.";

            count = 0;
            for (int i = 65; i <= 90; i++)
            {
                char c = (char)i;
                string str1 = c.ToString();
                if (password.Contains(str1))
                {
                    count = 1;
                    break;
                }
            }
            if (count == 0)
                return "Password should contain at least one uppercase letter.";

            count = 0;
            for (int i = 90; i <= 122; i++)
            {
                char c = (char)i;
                string str1 = c.ToString();

                if (password.Contains(str1))
                {
                    count = 1;
                    break;
                }
            }
            if (count == 0)
                return "Password should contain at least one lowercase letter.";

            return "Valid";
        }


        /// <summary>
        /// enable to press 'Ok' when all fields are full
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ok_IsEnabled(object sender, RoutedEventArgs e)
        {
            if (Username.Text.Length > 0 && Password.Password.Length > 0 && (!administrativePrivileges || ManagingCode.Password.Length > 0))
                Ok.IsEnabled = true;
            else
                Ok.IsEnabled = false;
        }        
    }
}
