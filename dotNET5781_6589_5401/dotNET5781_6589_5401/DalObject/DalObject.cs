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
            Bus clonedBus = bus.Clone();
            if (DS.DataSource.Buses.Exists(item => item.LicensePlate == clonedBus.LicensePlate))
                throw new BusException("The bus already exists.");
            DS.DataSource.Buses.Add(clonedBus);
        }
        void removeBus(Bus bus)
        {
            if (!DS.DataSource.Buses.Remove(bus))
                throw new BusException("The bus does not exist.");
        }
        void updateBus(Bus bus)
        {
            Bus clonedBus = bus.Clone();
            Bus busT;
            try
            {
                busT = (from item in DS.DataSource.Buses
                        where item.LicensePlate == clonedBus.LicensePlate
                        select item).First();
            }
            catch (ArgumentNullException)
            {
                throw new BusException("The bus does not exist.");
            }
            DS.DataSource.Buses.Remove(busT); // remove the old bus
            DS.DataSource.Buses.Add(clonedBus); // add the updated bus           
        }
        Bus getBus(string licensePlate)
        {
            Bus bus;
            try
            {
                bus = (from item in DS.DataSource.Buses
                       where item.LicensePlate == licensePlate
                       select item).First();
            }
            catch (ArgumentNullException)
            {
                throw new BusException("The bus does not exist.");
            }
            return bus;
        }
        IEnumerable<Bus> GetBuses()
        {
            IEnumerable<Bus> buses = from item in DS.DataSource.Buses
                                     select item.Clone();
            if (buses.Count() == 0)
                throw new BusException("No buses exist.");
            return buses;
        }
        IEnumerable<Bus> GetBuses(Predicate<Bus> condition)
        {
            IEnumerable<Bus> buses = from item in DS.DataSource.Buses
                                     where condition(item)
                                     select item.Clone();
            if (buses.Count() == 0)
                throw new BusException("No buses exist.");
            return buses;
        }

        #endregion

        #region Lines

        void addLine(Line line)
        {
            Line clonedLine = line.Clone();
            if (DS.DataSource.Lines.Exists(item => item.ThisSerial == clonedLine.ThisSerial))
                throw new LineException("The line already exists.");
            DS.DataSource.Lines.Add(clonedLine);
        }
        void removeLine(Line line)
        {
            if (!DS.DataSource.Lines.Remove(line))
                throw new LineException("The line does not exist.");
        }
        void updateLine(Line line)
        {
            Line clonedLine = line.Clone();
            Line lineT;
            try
            {
                lineT = (from item in DS.DataSource.Lines
                         where item.ThisSerial == clonedLine.ThisSerial
                         select item).First();
            }
            catch (ArgumentNullException)
            {
                throw new LineException("The line does not exist.");
            }
            DS.DataSource.Lines.Remove(lineT); // remove the old line
            DS.DataSource.Lines.Add(clonedLine); // add the updated line    
        }
        Line getLine(int serial)
        {
            Line line;
            try
            {
                line = (from item in DS.DataSource.Lines
                        where item.ThisSerial == serial
                        select item).First();
            }
            catch (ArgumentNullException)
            {
                throw new LineException("The line does not exist.");
            }
            return line;
        }
        IEnumerable<Line> GetLines()
        {
            IEnumerable<Line> lines = from item in DS.DataSource.Lines
                                     select item.Clone();
            if (lines.Count() == 0)
                throw new LineException("No lines exist.");
            return lines;
        }
        IEnumerable<Line> GetLines(Predicate<Line> condition)
        {
            IEnumerable<Line> lines = from item in DS.DataSource.Lines
                                      where condition(item)
                                      select item.Clone();
            if (lines.Count() == 0)
                throw new LineException("No lines exist.");
            return lines;
        }

        #endregion

        #region Stations

        void addStation(Station station)
        {
            Station clonedStation = station.Clone();
            if (DS.DataSource.Stations.Exists(item => item.ID == clonedStation.ID))
                throw new StationException("The station already exists.");
            DS.DataSource.Stations.Add(clonedStation);
        }
        void removeStation(Station station)
        {
            if (!DS.DataSource.Stations.Remove(station))
                throw new StationException("The station does not exist.");
        }
        void updateStation(Station station)
        {
            Station clonedStation = station.Clone();
            Station stationT;
            try
            {
                stationT = (from item in DS.DataSource.Stations
                            where item.ID == clonedStation.ID
                            select item).First();
            }
            catch (ArgumentNullException)
            {
                throw new LineException("The station does not exist.");
            }
            DS.DataSource.Stations.Remove(stationT); // remove the old station
            DS.DataSource.Stations.Add(clonedStation); // add the updated station
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
            IEnumerable<Station> stations =  from item in DS.DataSource.Stations
                                             select item.Clone();
            if (stations.Count() == 0)
                throw new StationException("No stations exist.");
            return stations;
        }
        IEnumerable<Station> GetStations(Predicate<Station> condition)
        {
            IEnumerable<Station> stations = from item in DS.DataSource.Stations
                                            where condition(item)
                                            select item.Clone();
            if (stations.Count() == 0)
                throw new StationException("No stations exist.");
            return stations;
        }

        #endregion

        #region LineStations

        void addLineStation(LineStation lineStation)
        {

        }
        void removeLineStation(LineStation lineStation)
        {

        }
        void updateLineStation(LineStation lineStation)
        {

        }

        LineStation getLineStation(int numberLine, int id)
        {

        }

        IEnumerable<Station> GetLineStations()
        {

        }

        IEnumerable<Station> GetLineStations(Predicate<LineStation> condition)
        {

        }


        #endregion

        #region FollowingStations

        void addTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {

        }

        void removeTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {

        }

        void updateTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {

        }

        TwoFollowingStations getTwoFollowingStations(int firstStationID, int secondStationID)
        {

        }

        IEnumerable<Station> GetFollowingStations()
        {

        }

        IEnumerable<Station> GetFollowingStations(Predicate<TwoFollowingStations> condition)
        {

        }


        #endregion

        #region DrivingBuses

        void addDrivingBus(DrivingBus drivingBus)
        {

        }

        void removeDrivingBus(DrivingBus drivingBus)
        {

        }

        DrivingBus getDrivingBus(int thisSerial, string licensePlate, int line, DateTime start)
        {

        }

        IEnumerable<Station> GetDrivingBuses()
        {

        }

        IEnumerable<Station> GetDrivingBuses(Predicate<DrivingBus> condition)
        {

        }


        #endregion

        #region DrivingLines

        void addDrivingLine(DrivingLine drivingLine)
        {

        }

        void removeDrivingLine(DrivingLine drivingLine)
        {

        }

        DrivingLine getDrivingLine(int numberLine, DateTime start)
        {

        }

        IEnumerable<Station> GetDrivingLines()
        {

        }

        IEnumerable<Station> GetDrivingLines(Predicate<DrivingLine> condition)
        {

        }


        #endregion
    }
}
