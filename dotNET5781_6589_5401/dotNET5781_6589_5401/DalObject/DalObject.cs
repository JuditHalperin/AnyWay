using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;

namespace DL
{
    public class DalObject : IDAL
    {
        #region Singelton

        static readonly DalObject instance = new DalObject();
        public static DalObject Instance => instance;

        static DalObject() { }
        DalObject() { }

        #endregion

        #region Buses

        void addBus(Bus bus)
        {
            if (!DS.DataSource.Buses.Exists(item => item.LicensePlate == bus.LicensePlate))
                throw new BusException("The bus already exists.");
            DS.DataSource.Buses.Add(bus);
        }
        void removeBus(Bus bus)
        {
            if(!DS.DataSource.Buses.Remove(bus))
                throw new BusException("The bus does not exist.");           
        }
        void updateBus(Bus bus)
        {
            Bus busT;
            try
            {
                busT = (from busTemp in DS.DataSource.Buses//Find the bus for update.
                                 where busTemp.LicensePlate == bus.LicensePlate
                                 select busTemp).First();
            }
            catch
            {
                throw new BusException("There is not exsits bus with same license of the bus for update.");
            }
            DS.DataSource.Buses.Remove(busT);//Remove the old bus
            DS.DataSource.Buses.Add(bus);//Add the update bus
        }
        Bus getBus(string licensePlate)
        {
            Bus bus;
            try
            {
                bus = (from busTemp in DS.DataSource.Buses
                           where busTemp.LicensePlate == licensePlate
                           select busTemp).First();
            }
            catch
            {
                throw new BusException("There is not exsits bus with this license.");
            }
            return bus;
        }
        IEnumerable<Bus> GetBuses()
        {
            return DS.DataSource.Buses;
        }
        IEnumerable<Bus> GetBuses(Predicate<Bus> condition)
        {
            IEnumerable<Bus> buses = from bus in DS.DataSource.Buses
                                     where condition(bus)
                                     select bus;
            return buses;
        }

        #endregion

        #region Lines

        void addLine(Line line)
        {
            if (!DS.DataSource.Lines.Exists(item => item.ThisSerial == line.ThisSerial))
                throw new LineException("The line already exists.");
            DS.DataSource.Lines.Add(line);
        }
        void removeLine(Line line)
        {
            if (!DS.DataSource.Lines.Remove(line))
                throw new LineException("The line does not exist.");
        }
        void updateLine(Line line)
        {
            Line lineT;
            try
            {
                lineT = (from lineTemp in DS.DataSource.Lines//Find the bus for update.
                             where lineTemp.ThisSerial == line.ThisSerial
                             select lineTemp).First();
            }
            catch (ArgumentNullException)
            {
                throw new LineException("There is not exsits line with same serial of the line for update.");
            }
            DS.DataSource.Lines.Remove(lineT);//Remove the old bus
            DS.DataSource.Lines.Add(line);//Add the update bus
        }
        Line getLine(int serial)
        {
            Line line;
            try
            {
                line = (from lineTemp in DS.DataSource.Lines//Find the bus for update.
                         where lineTemp.ThisSerial == serial
                         select lineTemp).First();
            }
            catch
            {
                throw new LineException("There is not exsits line with this serial.");
            }
            return line;
        }
        IEnumerable<Line> GetLines()
        {
            return DS.DataSource.Lines.Clone();
        }
        IEnumerable<Line> GetLines(Predicate<Line> condition)
        {
            IEnumerable<Line> lines = from line in DS.DataSource.Lines
                                     where condition(line)
                                     select line;
            return lines;
        }

        #endregion

        #region Stations

        void addStation(Station station)
        {
            Station clonedStation = station.Clone();
            if (DS.DataSource.Stations.Exists(item => item.ID == station.ID))
                throw new StationException("The station already exists.");
            DS.DataSource.Stations.Add(station);
        }
        void removeStation(Station station)
        {
            if (!DS.DataSource.Stations.Remove(station))
                throw new StationException("The station does not exist.");
        }
        void updateStation(Station station)
        {
            Station stationT;
            try
            {
                stationT = (from item in DS.DataSource.Stations
                            where item.ID == station.ID
                            select item).First();
            }
            catch (ArgumentNullException)
            {
                throw new LineException("The station does not exist.");
            }
            DS.DataSource.Stations.Remove(stationT); // remove the old station
            DS.DataSource.Stations.Add(station); // add the updated station
        }
        Station getStation(int id)
        {
            Station station;
            try
            {
                station = (from item in DS.DataSource.Stations
                           where item.ID == id
                           select item).First();
            }
            catch (ArgumentNullException)
            {
                throw new LineException("The station does not exist.");
            }
            return station;
        }
        IEnumerable<Station> GetStations()
        {
            return DS.DataSource.Stations;
        }
        IEnumerable<Station> GetStations(Predicate<Station> condition)
        {

        }

        #endregion

        #region LineStations

        void addLineStation(LineStation lineStation);
        void removeLineStation(LineStation lineStation);
        void updateLineStation(LineStation lineStation);
        LineStation getLineStation(int numberLine, int id);
        IEnumerable<Station> GetLineStations();
        IEnumerable<Station> GetLineStations(Predicate<LineStation> condition);

        #endregion

        #region FollowingStations

        void addTwoFollowingStations(TwoFollowingStations twoFollowingStations);
        void removeTwoFollowingStations(TwoFollowingStations twoFollowingStations);
        void updateTwoFollowingStations(TwoFollowingStations twoFollowingStations);
        TwoFollowingStations getTwoFollowingStations(int firstStationID, int secondStationID);
        IEnumerable<Station> GetFollowingStations();
        IEnumerable<Station> GetFollowingStations(Predicate<TwoFollowingStations> condition);

        #endregion

        #region DrivingBuses

        void addDrivingBus(DrivingBus drivingBus);
        void removeDrivingBus(DrivingBus drivingBus);
        DrivingBus getDrivingBus(int thisSerial, string licensePlate, int line, DateTime start);
        IEnumerable<Station> GetDrivingBuses();
        IEnumerable<Station> GetDrivingBuses(Predicate<DrivingBus> condition);

        #endregion

        #region DrivingLines

        void addDrivingLine(DrivingLine drivingLine);
        void removeDrivingLine(DrivingLine drivingLine);
        DrivingLine getDrivingLine(int numberLine, DateTime start);
        IEnumerable<Station> GetDrivingLines();
        IEnumerable<Station> GetDrivingLines(Predicate<DrivingLine> condition);

        #endregion
    }
}
