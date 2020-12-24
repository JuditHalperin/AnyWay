﻿using System;
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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        static IBL bl;
        User user;
        string gettingChanged;

        public ChangePassword(string description, string username)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            gettingChanged = description;
            Title = "Change" + gettingChanged;
            LabelOldPassword.Content = "Old" + gettingChanged + ":";
            LabelNewPassword.Content = "New" + gettingChanged + ":";
            user = bl.getUser(username);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gettingChanged == "password")
                {
                    if (user.Password != OldPassword.Password)
                        throw new InvalidInputException("Incorrect old password.");

                    string message = validPassword(NewPassword.Password);
                    if (message != "Valid")
                        throw new InvalidInputException(message);

                    bl.updateUser(new User()
                    {
                        Username = user.Username,
                        Password = NewPassword.Password,
                        IsManager = user.IsManager
                    });                    
                }
                
                else if (gettingChanged == "managing code")
                {
                    if (bl.getManagingCode() != OldPassword.Password)
                        throw new InvalidInputException("Incorrect old managing code.");

                    bl.updateManagingCode(NewPassword.Password);
                }

                Close();
            }

            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message);
            }  
            catch (UserException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
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
