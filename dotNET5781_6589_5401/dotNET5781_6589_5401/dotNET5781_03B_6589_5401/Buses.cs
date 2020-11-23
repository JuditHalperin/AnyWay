using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace dotNET5781_03B_6589_5401
{
    public class Buses
    {
        static private Random rand = new Random(DateTime.Now.Millisecond);

        private ObservableCollection<Bus> buses = new ObservableCollection<Bus>();

        public void InitializeBuses()
        {
            for (int i = 0; i < 7; i++)
                buses.Add(new Bus(
                    new DateTime(rand.Next(1990, 2017), rand.Next(1, 12), rand.Next(1, 30)),
                    new DateTime(rand.Next(2018, DateTime.Now.Year), rand.Next(1, 12), rand.Next(1, 30)),
                    Convert.ToString(rand.Next(1000000, 9999999)),
                    rand.Next(20000, 50000),
                    rand.Next(0, 1200),
                    rand.Next(0, 20000)));
            
        }


    }
}
