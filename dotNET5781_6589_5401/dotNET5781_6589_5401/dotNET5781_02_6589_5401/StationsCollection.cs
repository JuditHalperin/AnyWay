using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    public class StationsCollection
    {
        static public List<BusStation> stations;

        /// <summary>
        /// static constructor
        /// create 40 bus stations
        /// </summary>
        static StationsCollection()
        {
            stations = new List<BusStation>();
            for (int i = 0; i < 40; i++)
                stations.Add(new BusStation());
        }

        /// <summary>
        /// find a station in the list
        /// </summary>
        /// <param name="id">station ID</param>
        /// <returns>station of type 'BusStation'</returns>
        static public BusStation getStation(string id)
        {
            foreach (BusStation item in stations)         
                if (item.ID == id)
                    return item;
            return null;
        }
    }
}
