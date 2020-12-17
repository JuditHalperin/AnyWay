using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DS
{
    public static class DataSource
    {
        public static List<User> Users = new List<User>();
        public static List<Bus> Buses = new List<Bus>();
        public static List<Line> Lines = new List<Line>();
        public static List<Station> Stations = new List<Station>();
        public static List<LineStation> LineStations = new List<LineStation>();
        public static List<TwoFollowingStations> FollowingStations = new List<TwoFollowingStations>();
        public static List<DrivingBus> DrivingBuses = new List<DrivingBus>();
        public static List<DrivingLine> DrivingLines = new List<DrivingLine>();
        public static string ManagingCode = "123456";

        static DataSource()
        {
            //Users = new List<User>();
            //Buses = new List<Bus>();
            //Lines = new List<Line>();
            //Stations = new List<Station>();
            //LineStations = new List<LineStation>();
            //FollowingStations = new List<TwoFollowingStations>();
            //DrivingBuses = new List<DrivingBus>();
            //DrivingLines = new List<DrivingLine>();

            initializer();
        }

        private static void initializer()
        {

        }
    }
}