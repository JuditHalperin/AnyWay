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
        public void addStation(string stationID, int index)
        {

        }

        public void deleteStation(string stationID)
        {

        }

        private bool includesStation(string stationID)
        {

        }

        public double distanceBetweenTwoStations(string FirstID, string SecondID)
        {

        }

        public int MinutesBetweenTwoStations(string FirstID, string SecondID)
        {

        }
        /// <summary>
        /// create new BusLine with new list with the station from the: firstStationID until the: lastStationID.
        /// </summary>
        /// <param name="firstStationID">the station from it return the station of the bus</param>
        /// <param name="lastStationID">the last station. if this station not exist will be return list of station until the end station in the path.</param>
        /// <returns>bus with station from first until the last....</returns>
        public BusLine subPath(string firstStationID, string lastStationID)
        {
            List<BusLineStation> subPathBetweenTwoStations=new List<BusLineStation>();
            bool flag = false;
            foreach (BusLineStation station in path)
            {
                if (station.ID == firstStationID)
                    flag = true;
                if (flag)
                    subPathBetweenTwoStations.Add(station);
                if (station.ID == lastStationID)
                    break;
            }
            if (flag == false)
            {
                return null;//or exception
            }
            BusLine busOfSubPath = new BusLine(0, (int)Region, subPathBetweenTwoStations);
            return busOfSubPath;
        }//לזרוק חריגות. גם אם אין את האחרונה?

        public int CompareTo(BusLine secondBus)
        {

        }
    }
}
