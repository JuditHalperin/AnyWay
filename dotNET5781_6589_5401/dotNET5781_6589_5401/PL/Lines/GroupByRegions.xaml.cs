using BLAPI;
using BO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for GroupByRegions.xaml
    /// Group lines by their regions
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
                switch (groups[i].Key)
                {
                    case Regions.General:
                        General.ItemsSource = groups[i];
                        NoLinesInGeneral.Visibility = Visibility.Hidden;
                        break;

                    case Regions.Center:
                        Center.ItemsSource = groups[i];
                        NoLinesInCenter.Visibility = Visibility.Hidden;
                        break;

                    case Regions.South:
                        South.ItemsSource = groups[i];
                        NoLinesInSouth.Visibility = Visibility.Hidden;
                        break;

                    case Regions.North:
                        North.ItemsSource = groups[i];
                        NoLinesInNorth.Visibility = Visibility.Hidden;
                        break;

                    case Regions.Jerusalem:
                        Jerusalem.ItemsSource = groups[i];
                        NoLinesInJerusalem.Visibility = Visibility.Hidden;
                        break;
                }
        }

        private void Lines_MouseDoubleClick(object sender, SelectionChangedEventArgs e)
        {
            new LinesList(username, (int)(sender as ListBox).SelectedItem, administrativePrivileges).Show();
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
