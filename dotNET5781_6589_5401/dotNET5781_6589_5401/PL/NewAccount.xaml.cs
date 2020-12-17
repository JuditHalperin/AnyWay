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
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window
    {
        bool administrativePrivileges;

        public NewAccount(bool a)
        {
            InitializeComponent();
            administrativePrivileges = a;
            if (!a)
            {
                labelpassword.Visibility = Visibility.Hidden;
                PasswordM.Visibility = Visibility.Hidden;
            }

        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            string result = validPassword(Password.Password);

            if (result != "Valid")
                MessageBox.Show(result);
            else if (PasswordM.Password != "123456")
                MessageBox.Show("password for manage incorrect!");

            else
                try
                {
                    Users.addUser(Username.Text, Password.Password, administrativePrivileges);
                    new ManagerWindow(Username.Text).Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void ExistingUser_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            new SignInAsManager(administrativePrivileges).Show();
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
                String str1 = i.ToString();
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
                String str1 = c.ToString();
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
                String str1 = c.ToString();

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
    }
}
