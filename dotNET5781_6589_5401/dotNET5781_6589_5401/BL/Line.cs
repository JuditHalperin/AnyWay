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
        
        #endregion

        /// <summary>
        /// constructor - gets the first station
        /// </summary>
        /// <param name="firstStation">first station in path</param>
        public Line(int numberLine, Regions region, Station firstStation)
        {
            ThisSerial = serial++;
            NumberLine = numberLine;
            Region = region;

            //Path.Add(new LineStation(firstStation));
        }

        /// <summary>
        /// constructor - gets list of stations
        /// </summary>
        /// <param name="newPath">path</param>
        //public Line(ObservableCollection<LineStation> newPath) : this(newPath[0]) // call the first constructor
        //{
        //    for (int i = 1; i < newPath.Count(); i++)
        //        addStation(newPath[i], i + 1);
        //}

        /// <summary>
        /// constructor of a bus with sub-path
        /// it is not a real bus in the collection!
        /// its line number is the origin number * -1
        /// </summary>
        /// <param name="firstStation">first station of the sub-path</param>
        /// <param name="line">number of origin line</param>
        //private Line(Station firstStation, int line)
        //{
        //    NumberLine = -1 * line; // negative line indicates a sub-line
        //    Region = Regions.General;
        //    path.Add(new Station(firstStation.ID));
        //}

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
        /// add station to the given index in the list
        /// </summary>
        /// <param name="station">get existed station</param>
        /// <param name="index">index indicates where to place the station in the list (if index > size -> end of the list)</param>
        /// <returns>message about success</returns>
        public string addStation(Station station, int index)
        {
            index -= 1;

            LineStation newStation = new LineStation(station.ID);

            foreach (LineStation item in path)
                if (station.ID == item.ID)
                    throw new LineException("The station already exists in the path.");

            if (index > path.Count)
                index = path.Count;

            if (index >= 0)
            {
                if (index != 0) // not about to be inserted to the first place
                {
                    newStation.MetersFromLastStation = station.distanceBetweenStations(StationsCollection.getStation(path[index - 1].ID));
                    newStation.MinutesSinceLastStation = (int)(newStation.MetersFromLastStation * 0.001);
                }

                if (index != path.Count) // not about to be inserted to the last place
                {
                    path[index].MetersFromLastStation = station.distanceBetweenStations(StationsCollection.getStation(path[index].ID));
                    path[index].MinutesSinceLastStation = (int)(path[index].MetersFromLastStation * 0.001);

                    path.Insert(index, newStation);
                }

                else // add to the end of the list
                {
                    path.Add(newStation);
                    index = path.Count - 1;
                }

                return $"Station number {station.ID} was added successfully to index {index + 1} in the path of bus number {Line}.";
            }

            else
                throw new LineException("Invalid index.");
        }

        /// <summary>
        /// delete station from the path. if the station does not exist in the path -> throw exception.
        /// </summary>
        /// <param name="stationID">number of station to delete</param>
        /// <returns>message about success</returns>
        public string deleteStation(string stationID)
        {
            if (path.Count() <= 2)
                throw new LineException("It is impossible to remove a station if there are only two stations in the path.");

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

                if (i < path.Count() && i > 0)
                {
                    path[i].MetersFromLastStation = StationsCollection.getStation(path[i].ID).distanceBetweenStations(StationsCollection.getStation(path[i - 1].ID));
                    path[i].MinutesSinceLastStation = (int)(path[i].MetersFromLastStation * 0.001);
                }

                if (i == 0)
                {
                    path[i].MetersFromLastStation = 0;
                    path[i].MinutesSinceLastStation = 0;
                }

                return $"Station number {stationID} was removed successfully from the path of bus number {Line}.";
            }

            else
                throw new LineException("The station does not exist in this bus path.");
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
                throw new LineException("one of the station is not exist or the stations not in the true order.");

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
                throw new LineException("one of the station is not exist or the stations not in the true order.");

            return minutes;
        }

        /// <summary>
        /// create new BusLine with new list with the stations from the: firstStationID until the: lastStationID.
        /// </summary>
        /// <param name="firstStationID">the station from it return the stations of the bus</param>
        /// <param name="lastStationID">the last station. if this station not exist will be return list of station until the end station in the path.</param>
        /// <returns>bus with stations from first until the last....</returns>
        public Line subPath(string firstStationID, string lastStationID)
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

            Line busOfSubPath = new Line(path[firstIndex], NumberLine);

            for (int i = firstIndex + 1; i <= lastIndex; i++)
                busOfSubPath.path.Add(path[i]);

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
        public int CompareTo(Line secondBus)
        {
            return durationDrive().CompareTo(secondBus.durationDrive());
        }

    }
}


