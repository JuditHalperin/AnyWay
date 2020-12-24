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
using PO;
using BO;

namespace PL.Lines
{
    /// <summary>
    /// Interaction logic for LinesList.xaml
    /// </summary>
    public partial class LinesList : Window
    {
        static IBL bl;

        public LinesList(string username)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            ManagerWindow managerWindow = new ManagerWindow(username); // open when 'cancel' is clicked

            IEnumerable<BO.Line> lines = bl.GetLines();
            LinesList.ItemsSource = lines;
            if(lines.Count() > 0)
            {
                LinesList.SelectedIndex = 0;
                LinesList.Text = ((BO.Line)LinesList.SelectedItem).ThisSerial.ToString();
                selectionChanged();
            }          
        }

        private void selectionChanged()
        {
            DataContext = (BO.Line)LinesList.SelectedItem;
            NumberOfStations.Content = ((BO.Line)LinesList.SelectedItem).Path.Count();
            IEnumerable<LineStation> lineStations = ((BO.Line)LinesList.SelectedItem).Path;
            if (lineStations.Count() > 0)
            {
                NoStations.Visibility = Visibility.Hidden;
                LineStations.Visibility = Visibility.Visible;
            }
            else
            {
                LineStations.Visibility = Visibility.Hidden;
                NoStations.Visibility = Visibility.Visible;
            }
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            new AddLine().ShowDialog();
        }

        private void EditLine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch(LineException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
