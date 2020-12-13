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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace temp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void SignInAsPassenger_Click(object sender, RoutedEventArgs e)
        {
            SignInAsManager window = new SignInAsManager(false);
            window.Show();
            Close();
        }

        private void SignInAsManager_Click(object sender, RoutedEventArgs e)
        {
            SignInAsManager window = new SignInAsManager(true);
            window.Show();
            Close();
        }
    }
}
