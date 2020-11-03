using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    enum Regions { General, North, South, Center, Jerusalem }

    class BusLine : IComparable<BusLine>
    {
        private List<BusLineStation> path;
        public List<BusLineStation> Path
        {
            get { return path; }
            set { path = value; }
        }

        private int line;
        public int Line
        {
            get { return line; }
            set { line = value; }
        }

        private BusStation firstStation;
        public BusStation FirstStation
        {
            get { return firstStation; }
            set { firstStation = value; }
        }

        private BusStation lastStation;
        public BusStation LastStation
        {
            get { return firstStation; }
            set { firstStation = value; }
        }

        private Regions region;
        public Regions Region
        {
            get { return region; }
            set { region = value; }
        }
        /// <summary>
        /// constactor
        /// </summary>
        /// <param name="line">number of line</param>
        /// <param name="region">number that indicate region according to the Enum:"Regions". </param>
        /// <param name="path">list of the station of th bus</param>
        public BusLine(int line, int region, List<BusLineStation> path)
        {
            Line = line;
            if (region <= 4 && region >= 0)
                Region = (Regions)region;
            else
                Region = (Regions)0;
            Path = path;
        }
        /// <summary>
        /// ovarride about "ToString".
        /// </summary>
        /// <returns>string of: number of line, region of the activity of the line and the phat of the line in back and forth.</returns>
        public override string ToString()
        {
            string descriptionOfBus;
            descriptionOfBus = "Line:" + line + "\n";
            descriptionOfBus += "Line activity area:" + Region + "\n";
            descriptionOfBus += "The stations of the Line: (in back and forth)\n";
            foreach (BusLineStation station in path)
            {
                descriptionOfBus += "->" + station.ID;
            }
            string reverse = "\n";
            descriptionOfBus += "\nThe route is back:\n";
            foreach (BusLineStation station in path)//to chaining reverse
            {
                reverse = station.ID + "->" + reverse;
            }
            descriptionOfBus += reverse;
            return descriptionOfBus;
        }

        /// <summary>
        /// enable to add station to all space in the list.
        /// </summary>
        /// <param name="stationID">get id of exist station</param>
        /// <param name="index">index indicates where to place the station in the list (if index>size ->end of the list.)</param>
        public void addStation(BusLineStation station, int index)
        {
            if (index >= 0 && index < path.Count())
                path.Insert(index, station);
            else if (index > path.Count())
                path.Add(station);
            else
                throw new BusesOrStationsExceptions("index is not valid!");
        }
        /// <summary>
        /// delete station from the path. if the station not exist in the path-> throw exception.
        /// </summary>
        /// <param name="stationID">number of station to delete</param>
        public void deleteStation(string stationID)
        {
            int i = 0;
            foreach (BusLineStation station in path)
            {
                if (stationID == station.ID)
                    break;
                i++;
            }
            if (i < path.Count())
                path.Remove(path[i]);
            else
                throw new BusesOrStationsExceptions("the station is not exist in this path!");

        }
        /// <summary>
        /// chack if the station is exist in the path of the bus.
        /// </summary>
        /// <param name="stationID">number of station to check</param>
        /// <returns>true if the station is exist</returns>
        public bool stopsAtStation(string stationID)
        {
            foreach (BusLineStation station in path)
            {
                if (stationID == station.ID)
                    return true;
            }
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
            int minutes=0;
            foreach(BusLineStation station in path)
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
            List<BusLineStation> subPathBetweenTwoStations = new List<BusLineStation>();
            bool isExistFirst = false;
            bool isExistLast = false;
            foreach (BusLineStation station in path)
            {
                if (station.ID == firstStationID)
                    isExistFirst = true;
                if (isExistFirst)
                    subPathBetweenTwoStations.Add(station);
                if (station.ID == lastStationID && isExistFirst == true)
                {
                    isExistLast = true;
                    break;
                }
            }
            if (isExistFirst == false || isExistLast == false)
                throw new BusesOrStationsExceptions("Error!\none or more of the station is not exist or the stations not in the true order.");
            BusLine busOfSubPath = new BusLine(Line, (int)Region, subPathBetweenTwoStations);
            return busOfSubPath;
        }//לזרוק חריגות. גם אם אין את האחרונה?
        /// <summary>
        /// calculate time of drive
        /// </summary>
        /// <returns>time of drive</returns>
        private int durationDrive()
        {
            int minutes=0;
            foreach (BusLineStation station in path)
            {
                minutes += station.MinutesSinceLastStation;
            }
            return minutes;
        }
        /// <summary>
        /// Compares the travel time of two lines
        /// </summary>
        /// <param name="secondBus">line o comparetion</param>
        /// <returns>how is bigger.</returns>
        public int CompareTo(BusLine secondBus)
        {
           return durationDrive().CompareTo(secondBus.durationDrive());
        }
    }
}
