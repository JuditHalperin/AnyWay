using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    enum Regions { General, North, South, Center, Jerusalem}

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

        public BusLine(int line, Regions region, List<BusLineStation> path)
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }

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

        public BusLine subPath(string firstStationID, string lastStationID)
        {

        }

        public int CompareTo(BusLine secondBus)
        {

        }
    }
}
