using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace dotNET5781_03B_6589_5401
{
    public static class Buses
    {
        static private Random rand = new Random(DateTime.Now.Millisecond);

        static public ObservableCollection<Bus> buses = new ObservableCollection<Bus>();

        /// <summary>
        /// static constructor
        /// initialize 10 buses randomaly
        /// </summary>
        static Buses()
        {
            for (int i = 0; i < 7; i++)
                buses.Add(new Bus(
                    new DateTime(rand.Next(1990, 2017), rand.Next(1, 12), rand.Next(1, 28)),
                    new DateTime(rand.Next(2018, DateTime.Now.Year), rand.Next(1, 12), rand.Next(1, 28)),
                    Convert.ToString(rand.Next(1000000, 9999999)),
                    rand.Next(20000, 50000),
                    rand.Next(0, 1200),
                    rand.Next(0, 20000)));

            buses.Add(new Bus(
                    new DateTime(2019, 7, 8),
                    new DateTime(2020, 8, 5),
                    Convert.ToString(rand.Next(10000000, 99999999)),
                    rand.Next(20000, 50000),
                    rand.Next(1000, 1200),
                    rand.Next(0, 20000)));

            buses.Add(new Bus(
                    new DateTime(2020, 1, 1),
                    new DateTime(2020, 7, 19),
                    Convert.ToString(rand.Next(10000000, 99999999)),
                    rand.Next(20000, 50000),
                    rand.Next(0, 1200),
                    rand.Next(19500, 19900)));

            buses.Add(new Bus(
                    new DateTime(rand.Next(1990, 2017), rand.Next(1, 12), rand.Next(1, 28)),
                    new DateTime(DateTime.Now.Year - 1, rand.Next(1, DateTime.Now.Month), 1),
                    Convert.ToString(rand.Next(1000000, 9999999)),
                    rand.Next(20000, 50000),
                    rand.Next(0, 1200),
                    rand.Next(0, 20000)));
        }

    }
}
