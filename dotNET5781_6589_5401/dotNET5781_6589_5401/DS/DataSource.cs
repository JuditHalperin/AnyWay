using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    /// <summary>
    /// TO DO
    /// </summary>
    public static class DataSource
    {
        private static string helloStr = "Hello World";
        /// <summary>
        /// Bus repository
        /// </summary>
        private static List<BusDAO> busses = new List<BusDAO>();
        private static List<BusInTravelDAO> busestravel = new List<BusInTravelDAO>();

        public static List<BusDAO> Buses { get => busses; }
        public static List<BusInTravelDAO> BusesTravel { get => busestravel; }

        public static string Hello
        {
            get { return helloStr; }
            set { helloStr = value; }
        }

        public static void initBuses()
        {
            Buses.Add(new BusDAO
            {
                License = 1234567,
                StartOfWork = DateTime.Today.AddYears(-3),
                TotalKms = 5000,
                Fuel = 1200,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 3333333,
                StartOfWork = DateTime.Today.AddYears(-20),
                TotalKms = 9999999,
                Fuel = 500,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 77745617,
                StartOfWork = DateTime.Today.AddYears(-2),
                TotalKms = 5000,
                Fuel = 1200,
                Status = Status.READY
            });
            Buses.Add(new BusDAO
            {
                License = 6666666,
                StartOfWork = DateTime.Today.AddYears(-100),
                TotalKms = 5000,
                Fuel = 1200,
                Status = Status.READY
            });

        }
    }
}