using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using DLAPI;
using DO;
using DS;

namespace DL
{
    public sealed class DalObject : IDAL
    {
        #region Singelton

        static readonly DalObject instance = new DalObject();
        public static DalObject Instance => instance;
        static DalObject() { }
        DalObject() { }

        #endregion

        #region Users

        public void addUser(User user)
        {
            if (DataSource.Users.Exists(item => item.Username == user.Username))
                throw new UserException("The user already exists.");
            DataSource.Users.Add(user.Clone());
        }
        public void removeUser(User user)
        {
            if (!DataSource.Users.Remove(user))
                throw new UserException("The user does not exist.");
        }
        public void updateUser(User user)
        {
            User u = DataSource.Users.Find(item => item.Username == user.Username);
            if (u == null)
                throw new UserException("The user does not exist.");
            DataSource.Users.Remove(u); // remove the old user
            DataSource.Users.Add(user.Clone()); // add the updated user
        }
        public User getUser(string username)
        {
            User user = DataSource.Users.Find(item => item.Username == username);
            if (user == null)
                throw new UserException("The user does not exist.");
            return user.Clone();
        }
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = from item in DataSource.Users
                                      select item.Clone();
            if (users.Count() == 0)
                throw new UserException("No users exist.");
            return users;
        }
        public IEnumerable<User> GetUsers(Predicate<User> condition)
        {
            IEnumerable<User> users = from item in DataSource.Users
                                      where condition(item)
                                      select item.Clone();
            if (users.Count() == 0) 
                throw new UserException("No users exist.");
            return users;
        }

        #endregion

        #region Buses

        public void addBus(Bus bus)
        {
            if (DataSource.Buses.Exists(item => item.LicensePlate == bus.LicensePlate))
                throw new BusException("The bus already exists.");
            DataSource.Buses.Add(bus.Clone());
        }
        public void removeBus(Bus bus)
        {
            if (!DataSource.Buses.Remove(bus))
                throw new BusException("The bus does not exist.");
        }
        public void updateBus(Bus bus)
        {
            Bus b = DataSource.Buses.Find(item => item.LicensePlate == bus.LicensePlate);
            if (b == null)
                throw new BusException("The bus does not exist.");
            DataSource.Buses.Remove(b); // remove the old bus
            DataSource.Buses.Add(bus.Clone()); // add the updated bus            
        }
        public Bus getBus(string licensePlate)
        {
            Bus bus = DataSource.Buses.Find(item => item.LicensePlate == licensePlate);
            if (bus == null)
                throw new BusException("The bus does not exist.");
            return bus.Clone();
        }
        public IEnumerable<Bus> GetBuses()
        {
            return from item in DataSource.Buses
                   select item.Clone();
        }
        public IEnumerable<Bus> GetBuses(Predicate<Bus> condition)
        {
            IEnumerable<Bus> buses = from item in DataSource.Buses
                                     where condition(item)
                                     select item.Clone();
            if (buses.Count() == 0)
                throw new BusException("No buses exist.");
            return buses;
        }

        #endregion

        #region Lines

        public void addLine(Line line)
        {
            line.ThisSerial = DataSource.serial++;
            DataSource.Lines.Add(line.Clone());
        }
        public void removeLine(Line line)
        {
            if (!DataSource.Lines.Remove(line))
                throw new LineException("The line does not exist.");
        }
        public void updateLine(Line line)
        {
            Line l = DataSource.Lines.Find(item => item.ThisSerial == line.ThisSerial);
            if (l == null)
                throw new LineException("The line does not exist.");
            DataSource.Lines.Remove(l); // remove the old line
            DataSource.Lines.Add(line.Clone()); // add the updated line  
        }
        public Line getLine(int serial)
        {
            Line line = DataSource.Lines.Find(item => item.ThisSerial == serial);
            if (line == null)
                throw new LineException("The line does not exist.");
            return line.Clone();           
        }
        public IEnumerable<Line> GetLines()
        {
            return (from item in DataSource.Lines
                    select item.Clone()).OrderBy(item => item.NumberLine);
        }
        public IEnumerable<Line> GetLines(Predicate<Line> condition)
        {
            IEnumerable<Line> lines = from item in DataSource.Lines
                                      where condition(item)
                                      select item.Clone();
            if (lines.Count() == 0)
                throw new LineException("No lines exist.");
            return lines.OrderBy(item=>item.NumberLine);
        }

        #endregion

        #region Stations

        public void addStation(Station station)
        {
            if (DataSource.Stations.Exists(item => item.ID == station.ID))
                throw new StationException("The station already exists.");
            DataSource.Stations.Add(station.Clone());
        }
        public void removeStation(Station station)
        {
            if (!DataSource.Stations.Remove(station))
                throw new StationException("The station does not exist.");
        }
        public void updateStation(Station station)
        {
            Station s = DataSource.Stations.Find(item => item.ID == station.ID);
            if (s == null)
                throw new LineException("The station does not exist.");
            DataSource.Stations.Remove(s); // remove the old station
            DataSource.Stations.Add(station.Clone()); // add the updated station
        }
        public Station getStation(int id)
        {
            Station station = DataSource.Stations.Find(item => item.ID == id);
            if (station == null)
                throw new LineException("The station does not exist.");
            return station.Clone();
        }
        public IEnumerable<Station> GetStations()
        {
            return (from item in DataSource.Stations
                    select item.Clone()).OrderBy(item => item.ID);
        }
        public IEnumerable<Station> GetStations(Predicate<Station> condition)
        {
            IEnumerable<Station> stations = from item in DataSource.Stations
                                            where condition(item)
                                            select item.Clone();
            if (stations.Count() == 0)
                throw new StationException("No stations exist.");
            return stations.OrderBy(item => item.ID);
        }

        #endregion

        #region LineStations

        public void addLineStation(LineStation lineStation)
        {
            try // check if the station exists
            {
                getStation(lineStation.ID);
            }
            catch (StationException ex)
            {
                throw new StationException(ex.Message);
            }
            if (DataSource.LineStations.Exists(item => item.NumberLine == lineStation.NumberLine && item.ID == lineStation.ID))
                throw new StationException("The line station already exists.");
            DataSource.LineStations.Add(lineStation.Clone());
        }
        public void removeLineStation(LineStation lineStation)
        {
            if (!DataSource.LineStations.Remove(lineStation))
                throw new StationException("The line station does not exist.");            
        }
        public void updateLineStation(LineStation lineStation)
        {
            LineStation l = DataSource.LineStations.Find(item => item.NumberLine == lineStation.NumberLine && item.ID == lineStation.ID);
            if (l == null)
                throw new StationException("The line station does not exist.");
            DataSource.LineStations.Remove(l); // remove the old line station
            DataSource.LineStations.Add(lineStation.Clone()); // add the updated line station
        }
        public LineStation getLineStation(int numberLine, int id)
        {
            LineStation lineStation = DataSource.LineStations.Find(item => item.NumberLine == numberLine && item.ID == id);
            if (lineStation == null)
                throw new StationException("The line station does not exist.");
            return lineStation.Clone();            
        }
        public IEnumerable<LineStation> GetLineStations()
        {
            IEnumerable<LineStation> lineStations = from item in DataSource.LineStations
                                                    select item.Clone();
            if (lineStations.Count() == 0)
                throw new StationException("No line stations exist.");
            return lineStations;
        }
        public IEnumerable<LineStation> GetLineStations(Predicate<LineStation> condition)
        {
            IEnumerable<LineStation> lineStations = from item in DataSource.LineStations
                                                    where condition(item)
                                                    select item.Clone();
            if (lineStations.Count() == 0)
                throw new StationException("No line stations exist.");
            return lineStations;
        }

        #endregion

        #region FollowingStations

        public void addTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {
            if (twoFollowingStations.FirstStationID == twoFollowingStations.SecondStationID)
                throw new StationException("Two identical statuons.");
            if (DataSource.FollowingStations.Exists(item => (item.FirstStationID == twoFollowingStations.FirstStationID && item.SecondStationID == twoFollowingStations.SecondStationID) || (item.FirstStationID == twoFollowingStations.SecondStationID && item.SecondStationID == twoFollowingStations.FirstStationID)))
                throw new StationException("The two following stations already exist.");
            DataSource.FollowingStations.Add(twoFollowingStations.Clone());
        }
        public void removeTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {           
            if (!DataSource.FollowingStations.Remove(twoFollowingStations) && !DataSource.FollowingStations.Remove(new TwoFollowingStations() { FirstStationID = twoFollowingStations.SecondStationID, SecondStationID = twoFollowingStations.FirstStationID, LengthBetweenStations = twoFollowingStations.LengthBetweenStations, TimeBetweenStations = twoFollowingStations.TimeBetweenStations }))
                throw new StationException("The two following stations do not exist.");
        }
        public void updateTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {
            TwoFollowingStations f = DataSource.FollowingStations.Find(item => (item.FirstStationID == twoFollowingStations.FirstStationID && item.SecondStationID == twoFollowingStations.SecondStationID) || (item.FirstStationID == twoFollowingStations.SecondStationID && item.SecondStationID == twoFollowingStations.FirstStationID));
            if (f == null)
                throw new StationException("The two following stations do not exist.");            
            DataSource.FollowingStations.Remove(f); // remove the old two following stations
            DataSource.FollowingStations.Add(twoFollowingStations.Clone()); // add the updated two following stations
        }
        public TwoFollowingStations getTwoFollowingStations(int firstStationID, int secondStationID)
        {
            TwoFollowingStations twoFollowingStations = DataSource.FollowingStations.Find(item => (item.FirstStationID == firstStationID && item.SecondStationID == secondStationID) || (item.FirstStationID == secondStationID && item.SecondStationID == firstStationID));
            if (twoFollowingStations == null)
                throw new StationException("The two following stations do not exist.");
            return twoFollowingStations.Clone();
        }
        public IEnumerable<TwoFollowingStations> GetFollowingStations()
        {
            IEnumerable<TwoFollowingStations> followingStations = from item in DataSource.FollowingStations
                                                                  select item.Clone();
            if (followingStations.Count() == 0)
                throw new StationException("No following stations exist.");
            return followingStations.OrderBy(item=>item.TimeBetweenStations);
        }
        public IEnumerable<TwoFollowingStations> GetFollowingStations(Predicate<TwoFollowingStations> condition)
        {
            IEnumerable<TwoFollowingStations> followingStations = from item in DataSource.FollowingStations
                                                                  where condition(item)
                                                                  select item.Clone();
            if (followingStations.Count() == 0)
                throw new StationException("No following stations exist.");
            return followingStations.OrderBy(item => item.TimeBetweenStations);
        }

        #endregion

        #region DrivingBuses

        public void addDrivingBus(DrivingBus drivingBus)
        {
            try 
            {
                getBus(drivingBus.LicensePlate); // check if the bus exists
                drivingBus.ThisSerial = DataSource.serial++;
                DataSource.DrivingBuses.Add(drivingBus.Clone());
            }
            catch (BusException ex)
            { 
                throw new BusException(ex.Message); 
            }           
        }
        public void removeDrivingBus(DrivingBus drivingBus)
        {
            if (!DataSource.DrivingBuses.Remove(drivingBus))
                throw new BusException("The driving bus does not exist.");
        }
        public void updateDrivingBus(DrivingBus drivingBus)
        {
            DrivingBus d = DataSource.DrivingBuses.Find(item => item.ThisSerial == drivingBus.ThisSerial && item.LicensePlate == drivingBus.LicensePlate && item.Line == drivingBus.Line && item.Start == drivingBus.Start);
            if (d == null)
                throw new BusException("The driving bus does not exist.");
            DataSource.DrivingBuses.Remove(d); // remove the old driving bus
            DataSource.DrivingBuses.Add(drivingBus.Clone()); // add the updated driving bus  
        }
        public DrivingBus getDrivingBus(int thisSerial, string licensePlate, int line, DateTime start)
        {
            DrivingBus drivingBus = DataSource.DrivingBuses.Find(item => item.ThisSerial == thisSerial && item.LicensePlate == licensePlate && item.Line == line && item.Start == start);
            if (drivingBus == null)
                throw new BusException("The driving bus does not exist.");
            return drivingBus.Clone();
        }
        public IEnumerable<DrivingBus> GetDrivingBuses()
        {
            IEnumerable<DrivingBus> drivingBuses = from item in DataSource.DrivingBuses
                                                   select item.Clone();
            if (drivingBuses.Count() == 0)
                throw new BusException("No driving buses exist.");
            return drivingBuses;
        }
        public IEnumerable<DrivingBus> GetDrivingBuses(Predicate<DrivingBus> condition)
        {
            IEnumerable<DrivingBus> drivingBuses = from item in DataSource.DrivingBuses
                                                   where condition(item)
                                                   select item.Clone();
            if (drivingBuses.Count() == 0)
                throw new BusException("No driving buses exist.");
            return drivingBuses;
        }

        #endregion

        #region DrivingLines

        public void addDrivingLine(DrivingLine drivingLine)
        {
            try 
            {
                getLine(drivingLine.NumberLine); // check if the line exists
            }
            catch (LineException ex)
            {
                throw new LineException(ex.Message);
            }
            if (DataSource.DrivingLines.Exists(item => item.NumberLine == drivingLine.NumberLine && item.Start == drivingLine.Start))
                throw new LineException("The driving line already exists.");
            DataSource.DrivingLines.Add(drivingLine.Clone());
        }
        public void removeDrivingLine(DrivingLine drivingLine)
        {
            if (!DataSource.DrivingLines.Remove(drivingLine))
                throw new LineException("The driving line does not exist.");
        }
        public void updateDrivingLine(DrivingLine drivingLine)
        {
            DrivingLine d = DataSource.DrivingLines.Find(item => item.NumberLine == drivingLine.NumberLine && item.Start == drivingLine.Start);
            if (d == null)
                throw new LineException("The driving line does not exist.");           
            DataSource.DrivingLines.Remove(d); // remove the old driving line
            DataSource.DrivingLines.Add(drivingLine.Clone()); // add the updated driving line  
        }
        public DrivingLine getDrivingLine(int numberLine, DateTime start)
        {
            DrivingLine drivingLine = DataSource.DrivingLines.Find(item => item.NumberLine == numberLine && item.Start == start);
            if (drivingLine == null)
                throw new LineException("The driving line does not exist.");
            return drivingLine.Clone();
        }
        public IEnumerable<DrivingLine> GetDrivingLines()
        {
            IEnumerable<DrivingLine> drivingLines = from item in DataSource.DrivingLines
                                                    select item.Clone();
            if (drivingLines.Count() == 0)
                throw new LineException("No driving lines exist.");
            return drivingLines;
        }
        public IEnumerable<DrivingLine> GetDrivingLines(Predicate<DrivingLine> condition)
        {
            return from item in DataSource.DrivingLines
                   where condition(item)
                   select item.Clone();           
        }

        #endregion

        #region ManagingCode

        public string getManagingCode()
        {
            return DataSource.ManagingCode;
        }
        public void updateManagingCode(string code)
        {
            DataSource.ManagingCode = code;
        }

        #endregion
    }
}
