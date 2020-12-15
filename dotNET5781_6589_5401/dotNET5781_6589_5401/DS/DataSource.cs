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
        public static List<WindDirection> directions;//
        public static IEnumerable<User> users;

        static DataSource()
        {
            users = new IEnumerable<User>();
            directions = new List<WindDirection>();//
            directions.Add(new WindDirection());//
        }

    }
}