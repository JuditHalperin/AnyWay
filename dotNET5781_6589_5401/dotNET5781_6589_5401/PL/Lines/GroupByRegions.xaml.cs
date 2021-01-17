using BLAPI;
using BO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
//General, North, South, Center, Jerusalem
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

            List<IGrouping<Regions, int>> groups = bl.GetLinesByRegion().ToList();
            for (int i = 0; i < groups.Count(); i++)
            {
                Title = groups[i].Key;
                DataContext = groups[i];
                NoLines.Visibility = not;
            }
        }

        private void Lines_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new LinesList(username, (Line)(SelectedItem).ThisSerial, administrativePrivileges).Show();
            Close();
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
