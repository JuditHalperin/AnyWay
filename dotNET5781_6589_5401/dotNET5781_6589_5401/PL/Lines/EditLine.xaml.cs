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
using System.Collections.ObjectModel;
using BLAPI;
using PO;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for EditLine.xaml
    /// </summary>
    public partial class EditLine : Window
    {
        static IBL bl;

        ObservableCollection<Station> path = new ObservableCollection<Station>();

        public EditLine(BO.Line line)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();

            DataContext = line;
            path = (ObservableCollection<Station>)line.Path;
            LineStations.ItemsSource = path;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int line;
                if (LineNumber.Text.Length == 0 || !int.TryParse(LineNumber.Text, out line))
                    throw new InvalidInputException("Invalid format of line number.");
                if (path.Count() < 2)
                    throw new InvalidInputException("A line path should contain at least 2 stations.");
                //bl.updateLine(new BO.Line() { ThisSerial = ((BO.Line)DataContext).ThisSerial, NumberLine = line, Region = (Regions)RegionsList.SelectedItem, Path = path });
                Close();
            }
            catch (InvalidInputException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (LineException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveStation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
