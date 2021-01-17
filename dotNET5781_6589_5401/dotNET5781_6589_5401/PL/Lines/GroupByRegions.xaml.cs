using BLAPI;
using BO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for GroupByRegions.xaml
    /// </summary>
    public partial class GroupByRegions : Window
    {
        static IBL bl;
        string username;
        bool administrativePrivileges;

        public GroupByRegions(string name, bool a = true)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            username = name;
            administrativePrivileges = a;
        }

        private void Back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (administrativePrivileges)
                new LinesList(username, -1, true).Show();
            else
                new LinesList(username, -1, false).Show();
            Close();
        }
    }
}
