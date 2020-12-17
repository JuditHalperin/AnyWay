using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Line : IComparable<Line>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //static private Random rand = new Random(DateTime.Now.Millisecond);

        private static int serial = 1;

        #region 

        public ObservableCollection<LineStation> Path = new ObservableCollection<LineStation>();
        private int thisSerial; public int ThisSerial
        {
            get { return thisSerial; }
            private set { thisSerial = value; }
        }
        private int numberLine; public int NumberLine
        {
            get { return numberLine; }
            private set { numberLine = value; }
        }
        private Regions region; public Regions Region
        {
            get { return region; }
            set
            {
                region = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Region"));
            }
        }
        public int FirstStation
        {
            get { return Path.First().ID; }
        }
        public int LastStation
        {
            get { return Path.Last().ID; }
        }

        #endregion

        /// <summary>
        /// constructor - gets the first station
        /// </summary>
        /// <param name="firstStation">first station in path</param>
        public Line(int numberLine, Regions region, LineStation firstStation)
        {
            ThisSerial = serial++;
            NumberLine = numberLine;
            Region = region;

            Path.Add(firstStation);
        }

        /// <summary>
        /// constructor - gets list of stations
        /// </summary>
        /// <param name="newPath">path</param>
        public Line(int numberLine, Regions region, ObservableCollection<LineStation> newPath) : this(numberLine, region, newPath[0]) // call the first constructor
        {
            for (int i = 1; i < newPath.Count(); i++)
                Path.Add(newPath[i]);
        }

        /// <summary>
        /// copy constructor
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="numberLine"></param>
        /// <param name="region"></param>
        /// <param name="path"></param>
        public Line(int serial, int numberLine, Regions region, ObservableCollection<LineStation> path)
        {
            ThisSerial = serial;
            NumberLine = numberLine;
            Region = region;
            Path = path;
        }

        /// <summary>
        /// ovarride about "ToString".
        /// </summary>
        /// <returns>string of: number of line, region of the activity of the line and the phat of the line in back and forth.</returns>
        public override string ToString()
        {
            string descriptionOfBus = $"Line: {NumberLine}.    Region: {Region}.    Stations: ";

            foreach (LineStation station in Path)
                descriptionOfBus += station.ID + " -> ";

            descriptionOfBus = descriptionOfBus.Remove(descriptionOfBus.Length - 4, 4); // remove the last " -> "

            return descriptionOfBus;
        }
  

        /// <summary>
        /// chack if the station exists in the path of the bus.
        /// </summary>
        /// <param name="stationID">number of station to check</param>
        /// <returns>true if the station exists</returns>
        public bool stopsAtStation(int stationID)
        {
            foreach (LineStation station in Path)
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
        public double distanceBetweenTwoStations(int FirstID, int SecondID)
        {
            int fir = -1;
            int sec = -1;
            int i = 0;
            double meters = 0;

            foreach (LineStation station in Path)
            {
                if (FirstID == station.ID)
                    fir = i;

                if (fir > i)
                    meters += station.LengthFromPreviousStations;

                if (SecondID == station.ID)
                {
                    sec = i;
                    break;
                }

                i++;
            }

            if (fir == -1 || sec == -1)
                throw new LineException("one of the station is not exist or the stations not in the true order.");

            return meters;
        }

        /// <summary>
        /// Calculate travel time between two stations
        /// </summary>
        /// <param name="FirstID">start station to calculate</param>
        /// <param name="SecondID">end</param>
        /// <returns>the travel time between two stations</returns>
        public int MinutesBetweenTwoStations(int FirstID, int SecondID)
        {
            int fir = -1;
            int sec = -1;
            int i = 0;
            int minutes = 0;

            foreach (LineStation station in Path)
            {
                if (FirstID == station.ID)
                    fir = i;

                if (fir > i)
                    minutes += station.TimeFromPreviousStations;

                if (SecondID == station.ID)
                {
                    sec = i;
                    break;
                }

                i++;
            }

            if (fir == -1 || sec == -1)
                throw new LineException("one of the station is not exist or the stations not in the true order.");

            return minutes;
        }


        /// <summary>
        /// calculate time of drive
        /// </summary>
        /// <returns>time of drive</returns>
        private int durationDrive()
        {
            int minutes = 0;

            foreach (LineStation station in Path)
                minutes += station.TimeFromPreviousStations;

            return minutes;
        }

        /// <summary>
        /// compare time of travel of two lines
        /// </summary>
        /// <param name="secondBus">line compared to current line</param>
        /// <returns>whether or not this bus drive longer time than the other bus</returns>
        public int CompareTo(Line secondBus)
        {
            return durationDrive().CompareTo(secondBus.durationDrive());
        }

    }
}


