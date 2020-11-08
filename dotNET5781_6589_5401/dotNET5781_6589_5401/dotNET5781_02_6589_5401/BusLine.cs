using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    enum Regions { General, North, South, Center, Jerusalem }

    class BusLine : IComparable<BusLine>
    {
        static private Random rand = new Random(DateTime.Now.Millisecond);

        private static int code = 1;
         
        public class BusLineStation : BusStation
        {
            private double metersFromLastStation;
            public double MetersFromLastStation
            {
                get { return metersFromLastStation; }
                internal set { metersFromLastStation = value; }
            }

            private int minutesSinceLastStation;
            public int MinutesSinceLastStation
            {
                get { return minutesSinceLastStation; }
                internal set { minutesSinceLastStation = value; }
            }

            /// <summary>
            /// constructor
            /// (use the base class contructor, and initialize its two attributes by defualt)
            /// </summary>
            public BusLineStation(string id, double latitude, double longitude) : base(id, latitude, longitude) { }
        }

        private List<BusLineStation> path = new List<BusLineStation>();

        private int line;
        public int Line
        {
            get { return line; }
            private set { line = value; }
        }

        public BusStation FirstStation
        {
            get { return path.First(); }
        }

        public BusStation LastStation
        {
            get { return path.Last(); }
        }

        private Regions region;
        public Regions Region
        {
            get { return region; }
            set { region = value; }
        }

        /// <summary>
        /// constructor - gets the first station
        /// </summary>
        /// <param name="firstStation">first station in path</param>
        public BusLine(BusStation firstStation)
        {
            Line = code++;
            Region = (Regions)rand.Next(4);
            path.Add(new BusLineStation(firstStation.ID, firstStation.Latitude, firstStation.Longitude));
        }

        /// <summary>
        /// constructor - gets list of stations
        /// </summary>
        /// <param name="newPath">path</param>
        public BusLine(List<BusStation> newPath)
        {
            Line = code++;
            Region = (Regions)rand.Next(4);
            path.Add(new BusLineStation(newPath[0].ID, newPath[0].Latitude, newPath[0].Longitude));

            for (int i = 1; i <= newPath.Count(); i++)
                addStation(newPath[i - 1], i);
        }

        /// <summary>
        /// constructor of a bus with sub-path
        /// it is not a real bus in the collection!
        /// its line number is the origin number * -1
        /// </summary>
        /// <param name="firstStation">first station of the sub-path</param>
        /// <param name="line">number of origin line</param>
        private BusLine(BusStation firstStation, int line)
        {
            Line = -1 * line; // negative line indicates a sub-line
            Region = (Regions)rand.Next(4);
            path.Add(new BusLineStation(firstStation.ID, firstStation.Latitude, firstStation.Longitude));
        }

        /// <summary>
        /// ovarride about "ToString".
        /// </summary>
        /// <returns>string of: number of line, region of the activity of the line and the phat of the line in back and forth.</returns>
        public override string ToString()
        {
            string descriptionOfBus = $"Line: {Line}. Region: {Region}. Stations: ";

            foreach (BusLineStation station in path)
                descriptionOfBus += station.ID + " -> ";

            descriptionOfBus.Remove(descriptionOfBus.Length - 3, 4); // remove the last " -> "

            return descriptionOfBus;
        }
        
        /// <summary>
        /// add station to the given index in the list
        /// </summary>
        /// <param name="station">get existed station</param>
        /// <param name="index">index indicates where to place the station in the list (if index > size -> end of the list)</param>
        /// <returns>message about success</returns>
        public string addStation(BusStation station, int index)
        {
            BusLineStation newStation = new BusLineStation(station.ID, station.Latitude, station.Longitude);
            GeoCoordinate positionNewStation = new GeoCoordinate(newStation.Latitude, newStation.Longitude);

            if (index > path.Count)
                index = path.Count;

            if (index >= 0)
            {
                if (index != 0) // not to the first place
                {
                    GeoCoordinate positionPrevStation = new GeoCoordinate(path[index - 1].Latitude, path[index - 1].Longitude);
                    newStation.MetersFromLastStation = positionNewStation.GetDistanceTo(positionPrevStation);
                    newStation.MinutesSinceLastStation = (int)(newStation.MetersFromLastStation * 0.01);

                }

                if (index != path.Count) // not to the last place
                {
                    GeoCoordinate positionNextStation = new GeoCoordinate(path[index].Latitude, path[index].Longitude);
                    path[index].MetersFromLastStation = positionNewStation.GetDistanceTo(positionNextStation);
                    path[index].MinutesSinceLastStation = (int)(path[index].MetersFromLastStation * 0.01);

                    path.Insert(index, newStation);
                }

                else // add to the end of the list
                {
                    path.Add(newStation);
                    index = path.Count;
                }

                return $"Station number {station.ID} was added successfully to index {index} in the path of bus number {Line}.";
            }

            else
                throw new BusesOrStationsExceptions("Invalid index.");
        }

        /// <summary>
        /// delete station from the path. if the station does not exist in the path -> throw exception.
        /// </summary>
        /// <param name="stationID">number of station to delete</param>
        /// <returns>message about success</returns>
        public string deleteStation(string stationID)
        {
            int i = 0;

            foreach (BusLineStation station in path)
            {
                if (stationID == station.ID)
                    break;
                i++;
            }

            if (i < path.Count())
            {
                path.Remove(path[i]);

                if (i<path.Count())
                {
                    GeoCoordinate positionNextStation = new GeoCoordinate(path[i].Latitude, path[i].Longitude);
                    GeoCoordinate positionPrevStation = new GeoCoordinate(path[i - 1].Latitude, path[i - 1].Longitude);
                    path[i].MetersFromLastStation = positionNextStation.GetDistanceTo(positionPrevStation);
                    path[i].MinutesSinceLastStation = (int)(path[i].MetersFromLastStation * 0.01);
                }

                return $"Station number {stationID} was removed successfully from the path of bus {Line}.";
            }

            else
                throw new BusesOrStationsExceptions("The station does not exist in this bus path.");
        }

        /// <summary>
        /// chack if the station exists in the path of the bus.
        /// </summary>
        /// <param name="stationID">number of station to check</param>
        /// <returns>true if the station exists</returns>
        public bool stopsAtStation(string stationID)
        {
            foreach (BusLineStation station in path)            
                if (stationID == station.ID)
                    return true;
            
            return false;
        }

        /// <summary>
        /// Calculate travel distance between two stations
        /// </summary>
        /// <param name="FirstID">start station to calculate</param>
        /// <param name="SecondID">end</param>
        /// <returns>the travel distance between two stations</returns>
        public double distanceBetweenTwoStations(string FirstID, string SecondID)
        {
            int fir = -1;
            int sec = -1;
            int i = 0;
            double meters = 0;

            foreach (BusLineStation station in path)
            {
                if (FirstID == station.ID)
                    fir = i;

                if (fir > i)
                    meters += station.MetersFromLastStation;

                if (SecondID == station.ID)
                {
                    sec = i;
                    break;
                }

                i++;
            }

            if (fir == -1 || sec == -1)
                throw new BusesOrStationsExceptions("one of the station is not exist or the stations not in the true order.");
            
            return meters;
        }

        /// <summary>
        /// Calculate travel time between two stations
        /// </summary>
        /// <param name="FirstID">start station to calculate</param>
        /// <param name="SecondID">end</param>
        /// <returns>the travel time between two stations</returns>
        public int MinutesBetweenTwoStations(string FirstID, string SecondID)
        {
            int fir = -1;
            int sec = -1;
            int i = 0;
            int minutes = 0;

            foreach (BusLineStation station in path)
            {
                if (FirstID == station.ID)
                    fir = i;

                if (fir > i)
                    minutes += station.MinutesSinceLastStation;

                if (SecondID == station.ID)
                {
                    sec = i;
                    break;
                }

                i++;
            }

            if (fir == -1 || sec == -1)
                throw new BusesOrStationsExceptions("one of the station is not exist or the stations not in the true order.");
            
            return minutes;
        }

        /// <summary>
        /// create new BusLine with new list with the stations from the: firstStationID until the: lastStationID.
        /// </summary>
        /// <param name="firstStationID">the station from it return the stations of the bus</param>
        /// <param name="lastStationID">the last station. if this station not exist will be return list of station until the end station in the path.</param>
        /// <returns>bus with stations from first until the last....</returns>
        public BusLine subPath(string firstStationID, string lastStationID)
        {
            int firstIndex = -1;
            int lastIndex = -1;
            int index = 0;

            foreach (BusLineStation station in path)
            {
                if (station.ID == firstStationID)
                    firstIndex = index;
               
                if (station.ID == lastStationID)
                {
                    if (firstIndex != -1)
                        lastIndex = index;
                    break;
                }

                index++;
            }

            if (lastIndex == -1)
                return null; // at least one of the station does not exist, or the given order of stations in wrong

            BusLine busOfSubPath = new BusLine(path[firstIndex], Line);

            for (int i = firstIndex + 1; i <= lastIndex; i++)
                busOfSubPath.addStation(path[i], i - firstIndex);

            return busOfSubPath;
        }

        /// <summary>
        /// calculate time of drive
        /// </summary>
        /// <returns>time of drive</returns>
        private int durationDrive()
        {
            int minutes = 0;

            foreach (BusLineStation station in path)
                minutes += station.MinutesSinceLastStation;
          
            return minutes;
        }

        /// <summary>
        /// compare time of travel of two lines
        /// </summary>
        /// <param name="secondBus">line compared to current line</param>
        /// <returns>whether or not this bus drive longer time than the other bus</returns>
        public int CompareTo(BusLine secondBus)
        {
            return durationDrive().CompareTo(secondBus.durationDrive());
        }
    }
}