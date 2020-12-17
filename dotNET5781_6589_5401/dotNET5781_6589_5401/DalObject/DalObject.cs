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

        #region Users

        public void addUser(User user)
        {
            User clonedUser = user.Clone();
            if (DS.DataSource.Users.Exists(item => item.Username == clonedUser.Username))
                throw new UserException("The user already exists.");
            DS.DataSource.Users.Add(clonedUser);
        }
        public void removeUser(User user)
        {
            if (!DS.DataSource.Users.Remove(user))
                throw new UserException("The user does not exist.");
        }
        public void updateUser(User user)
        {
            User u = DataSource.Users.Find(item => item.Username == user.Username);
            if (u == null)
                throw new UserException("The user does not exist.");
            DS.DataSource.Users.Remove(u); // remove the old user
            DS.DataSource.Users.Add(user.Clone()); // add the updated user
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
            IEnumerable<User> users = from item in DS.DataSource.Users
                                      select item.Clone();
            if (users.Count() == 0)
                throw new UserException("No users exist.");
            return users;
        }
        public IEnumerable<User> GetUsers(Predicate<User> condition)
        {
            IEnumerable<User> users = from item in DS.DataSource.Users
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
            Bus clonedBus = bus.Clone();
            if (DS.DataSource.Buses.Exists(item => item.LicensePlate == clonedBus.LicensePlate))
                throw new BusException("The bus already exists.");
            DS.DataSource.Buses.Add(clonedBus);
        }
        public void removeBus(Bus bus)
        {
            if (!DS.DataSource.Buses.Remove(bus))
                throw new BusException("The bus does not exist.");
        }
        public void updateBus(Bus bus)
        {
            Bus b = DataSource.Buses.Find(item => item.LicensePlate == bus.LicensePlate);
            if (b == null)
                throw new BusException("The bus does not exist.");
            DS.DataSource.Buses.Remove(b); // remove the old bus
            DS.DataSource.Buses.Add(bus.Clone()); // add the updated bus            
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
            IEnumerable<Bus> buses = from item in DS.DataSource.Buses
                                     select item.Clone();
            if (buses.Count() == 0)
                throw new BusException("No buses exist.");
            return buses;
        }
        public IEnumerable<Bus> GetBuses(Predicate<Bus> condition)
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

        public void addLine(Line line)
        {
            Line clonedLine = line.Clone();
            if (DS.DataSource.Lines.Exists(item => item.ThisSerial == clonedLine.ThisSerial))
                throw new LineException("The line already exists.");
            DS.DataSource.Lines.Add(clonedLine);
        }
        public void removeLine(Line line)
        {
            if (!DS.DataSource.Lines.Remove(line))
                throw new LineException("The line does not exist.");
        }
        public void updateLine(Line line)
        {
            Line l = DataSource.Lines.Find(item => item.ThisSerial == line.ThisSerial);
            if (l == null)
                throw new LineException("The line does not exist.");
            DS.DataSource.Lines.Remove(l); // remove the old line
            DS.DataSource.Lines.Add(line.Clone()); // add the updated line  
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
            IEnumerable<Line> lines = from item in DS.DataSource.Lines
                                     select item.Clone();
            if (lines.Count() == 0)
                throw new LineException("No lines exist.");
            return lines;
        }
        public IEnumerable<Line> GetLines(Predicate<Line> condition)
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

        public void addStation(Station station)
        {
            Station clonedStation = station.Clone();
            if (DS.DataSource.Stations.Exists(item => item.ID == clonedStation.ID))
                throw new StationException("The station already exists.");
            DS.DataSource.Stations.Add(clonedStation);
        }
        public void removeStation(Station station)
        {
            if (!DS.DataSource.Stations.Remove(station))
                throw new StationException("The station does not exist.");
        }
        public void updateStation(Station station)
        {
            Station s = DataSource.Stations.Find(item => item.ID == station.ID);
            if (s == null)
                throw new LineException("The station does not exist.");
            DS.DataSource.Stations.Remove(s); // remove the old station
            DS.DataSource.Stations.Add(station.Clone()); // add the updated station
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
            IEnumerable<Station> stations =  from item in DS.DataSource.Stations
                                             select item.Clone();
            if (stations.Count() == 0)
                throw new StationException("No stations exist.");
            return stations;
        }
        public IEnumerable<Station> GetStations(Predicate<Station> condition)
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

        public void addLineStation(LineStation lineStation)
        {
            LineStation clonedLineStation = lineStation.Clone();
            if (DS.DataSource.LineStations.Exists(item => item.NumberLine == clonedLineStation.NumberLine && item.ID == clonedLineStation.ID))
                throw new StationException("The line station already exists.");
            DS.DataSource.LineStations.Add(clonedLineStation);
        }
        public void removeLineStation(LineStation lineStation)
        {
            if (!DS.DataSource.LineStations.Remove(lineStation))
                throw new StationException("The line station does not exist.");
        }
        public void updateLineStation(LineStation lineStation)
        {
            LineStation l = DataSource.LineStations.Find(item => item.NumberLine == lineStation.NumberLine && item.ID == lineStation.ID);
            if (l == null)
                throw new StationException("The line station does not exist.");
            DS.DataSource.LineStations.Remove(l); // remove the old line station
            DS.DataSource.LineStations.Add(lineStation.Clone()); // add the updated line station
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
            IEnumerable<LineStation> lineStations = from item in DS.DataSource.LineStations
                                                    select item.Clone();
            if (lineStations.Count() == 0)
                throw new StationException("No line stations exist.");
            return lineStations;
        }
        public IEnumerable<LineStation> GetLineStations(Predicate<LineStation> condition)
        {
            IEnumerable<LineStation> lineStations = from item in DS.DataSource.LineStations
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
            TwoFollowingStations clonedTwoFollowingStations = twoFollowingStations.Clone();
            if (DS.DataSource.FollowingStations.Exists(item => item.FirstStationID == clonedTwoFollowingStations.FirstStationID && item.SecondStationID == clonedTwoFollowingStations.SecondStationID))
                throw new StationException("The two following stations already exist.");
            DS.DataSource.FollowingStations.Add(clonedTwoFollowingStations);
        }
        public void removeTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {
            if (!DS.DataSource.FollowingStations.Remove(twoFollowingStations))
                throw new StationException("The two following stations do not exist.");
        }
        public void updateTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {
            TwoFollowingStations f = DataSource.FollowingStations.Find(item => item.FirstStationID == twoFollowingStations.FirstStationID && item.SecondStationID == twoFollowingStations.SecondStationID);
            if (f == null)
                throw new StationException("The two following stations do not exist.");            
            DS.DataSource.FollowingStations.Remove(f); // remove the old two following stations
            DS.DataSource.FollowingStations.Add(twoFollowingStations.Clone()); // add the updated two following stations
        }
        public TwoFollowingStations getTwoFollowingStations(int firstStationID, int secondStationID)
        {
            TwoFollowingStations twoFollowingStations = DataSource.FollowingStations.Find(item => item.FirstStationID == firstStationID && item.SecondStationID == secondStationID);
            if (twoFollowingStations == null)
                throw new StationException("The two following stations do not exist.");
            return twoFollowingStations.Clone();
        }
        public IEnumerable<TwoFollowingStations> GetFollowingStations()
        {
            IEnumerable<TwoFollowingStations> followingStations = from item in DS.DataSource.FollowingStations
                                                                  select item.Clone();
            if (followingStations.Count() == 0)
                throw new StationException("No following stations exist.");
            return followingStations;
        }
        public IEnumerable<TwoFollowingStations> GetFollowingStations(Predicate<TwoFollowingStations> condition)
        {
            IEnumerable<TwoFollowingStations> followingStations = from item in DS.DataSource.FollowingStations
                                                                  where condition(item)
                                                                  select item.Clone();
            if (followingStations.Count() == 0)
                throw new StationException("No following stations exist.");
            return followingStations;
        }

        #endregion

        #region DrivingBuses

        public void addDrivingBus(DrivingBus drivingBus)
        {
            DrivingBus clonedDrivingBus = drivingBus.Clone();
            if (DS.DataSource.DrivingBuses.Exists(item => item.ThisSerial == clonedDrivingBus.ThisSerial && item.LicensePlate == clonedDrivingBus.LicensePlate && item.Line == clonedDrivingBus.Line && item.Start == clonedDrivingBus.Start))
                throw new BusException("The driving bus already exists.");
            DS.DataSource.DrivingBuses.Add(clonedDrivingBus);
        }
        public void removeDrivingBus(DrivingBus drivingBus)
        {
            if (!DS.DataSource.DrivingBuses.Remove(drivingBus))
                throw new BusException("The driving bus does not exist.");
        }
        public void updateDrivingBus(DrivingBus drivingBus)
        {
            DrivingBus d = DataSource.DrivingBuses.Find(item => item.ThisSerial == drivingBus.ThisSerial && item.LicensePlate == drivingBus.LicensePlate && item.Line == drivingBus.Line && item.Start == drivingBus.Start);
            if (d == null)
                throw new BusException("The driving bus does not exist.");
            DS.DataSource.DrivingBuses.Remove(d); // remove the old driving bus
            DS.DataSource.DrivingBuses.Add(drivingBus.Clone()); // add the updated driving bus  
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
            IEnumerable<DrivingBus> drivingBuses = from item in DS.DataSource.DrivingBuses
                                                   select item.Clone();
            if (drivingBuses.Count() == 0)
                throw new BusException("No driving buses exist.");
            return drivingBuses;
        }
        public IEnumerable<DrivingBus> GetDrivingBuses(Predicate<DrivingBus> condition)
        {
            IEnumerable<DrivingBus> drivingBuses = from item in DS.DataSource.DrivingBuses
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
            DrivingLine clonedDrivingLine = drivingLine.Clone();
            if (DS.DataSource.DrivingLines.Exists(item => item.NumberLine == clonedDrivingLine.NumberLine && item.Start == clonedDrivingLine.Start))
                throw new LineException("The driving line already exists.");
            DS.DataSource.DrivingLines.Add(clonedDrivingLine);
        }
        public void removeDrivingLine(DrivingLine drivingLine)
        {
            if (!DS.DataSource.DrivingLines.Remove(drivingLine))
                throw new LineException("The driving line does not exist.");
        }
        public void updateDrivingLine(DrivingLine drivingLine)
        {
            DrivingLine d = DataSource.DrivingLines.Find(item => item.NumberLine == drivingLine.NumberLine && item.Start == drivingLine.Start);
            if (d == null)
                throw new LineException("The driving line does not exist.");           
            DS.DataSource.DrivingLines.Remove(d); // remove the old driving line
            DS.DataSource.DrivingLines.Add(drivingLine.Clone()); // add the updated driving line  
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
            IEnumerable<DrivingLine> drivingLines = from item in DS.DataSource.DrivingLines
                                                    select item.Clone();
            if (drivingLines.Count() == 0)
                throw new LineException("No driving lines exist.");
            return drivingLines;
        }
        public IEnumerable<DrivingLine> GetDrivingLines(Predicate<DrivingLine> condition)
        {
            IEnumerable<DrivingLine> drivingLines = from item in DS.DataSource.DrivingLines
                                                    where condition(item)
                                                    select item.Clone();
            if (drivingLines.Count() == 0)
                throw new LineException("No driving lines exist.");
            return drivingLines;
        }

        #endregion
    }
}
