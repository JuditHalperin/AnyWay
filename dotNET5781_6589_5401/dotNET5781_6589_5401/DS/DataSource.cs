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
        public static List<User> Users;
        public static List<Bus> Buses;
        public static List<Line> Lines;
        public static List<Station> Stations;
        public static List<LineStation> LineStations;
        public static List<TwoFollowingStations> FollowingStations;
        public static List<DrivingBus> DrivingBuses;
        public static List<DrivingLine> DrivingLines;

        static DataSource()
        {
            Users = new List<User>();
            Buses = new List<Bus>();
            Lines = new List<Line>();
            Stations = new List<Station>();
            LineStations = new List<LineStation>();
            FollowingStations = new List<TwoFollowingStations>();
            DrivingBuses = new List<DrivingBus>();
            DrivingLines = new List<DrivingLine>();

            // add to lists
        }

    }
}