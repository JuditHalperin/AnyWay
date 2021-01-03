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

namespace PL
{
    /// <summary>
    /// Interaction logic for SetDistances.xaml
    /// </summary>
    public partial class SetDistances : Window
    {
        static IBL bl;

        public SetDistances(Station station)
        {
            InitializeComponent();
            bl = BlFactory.GetBl();
            DataContext = station;
        }
    }
}
