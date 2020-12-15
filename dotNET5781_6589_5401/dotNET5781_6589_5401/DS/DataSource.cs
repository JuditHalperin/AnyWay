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
        public static List<User> users;
        public static List<Station> stations;
        public static List<LineStation> lineStations;
        public static List<TwoFollowingStations> followingStationsCouples;
        public static List<Bus> buses;
        public static List<DrivingBus> drivingBuses;
        public static List<Line> lines;
        public static List<DrivingLine> drivingLines;

        public static List<WindDirection> directions;//

        static DataSource()
        {
            users = new List<User>();
            stations = new List<Station>();
            lineStations = new List<LineStation>();
            followingStationsCouples = new List<TwoFollowingStations>();
            buses = new List<Bus>();
            drivingBuses = new List<DrivingBus>();
            lines = new List<Line>();
            drivingLines = new List<DrivingLine>();

            directions = new List<WindDirection>();//
            directions.Add(new WindDirection());//
        }

    }
}