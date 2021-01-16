using DLAPI;
using DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                DataSource.Users.RemoveAll(item=>item.Username==user.Username);
            }
            catch(ArgumentNullException)
            {
                throw new UserException("The user does not exist.");
            }
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
            User user= DataSource.Users.Find(item => item.Username == username);
            if (user == null)
                return null;
            return user.Clone();
        }
        public IEnumerable<User> GetUsers()
        {
            return from item in DataSource.Users
                   select item.Clone();
        }
        public IEnumerable<User> GetUsers(Predicate<User> condition)
        {
            return from item in DataSource.Users
                   where condition(item)
                   select item.Clone();
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
            Bus b = DataSource.Buses.Find(item => item.LicensePlate == bus.LicensePlate);
            if (b == null)
                throw new BusException("The bus does not exist.");
            DataSource.Buses.Remove(b);
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
                return null;
            return bus.Clone();
        }
        public IEnumerable<Bus> GetBuses()
        {
            return from item in DataSource.Buses
                   select item.Clone();           
        }
        public IEnumerable<Bus> GetBuses(Predicate<Bus> condition)
        {
            return from item in DataSource.Buses
                   where condition(item)
                   select item.Clone();
        }

        #endregion

        #region Lines

        public int addLine(Line line)
        {
            line.ThisSerial = DataSource.serial++;
            DataSource.Lines.Add(line.Clone());
            return line.ThisSerial;
        }
        public void removeLine(Line line)
        {
            Line l = DataSource.Lines.Find(item => item.ThisSerial == line.ThisSerial);
            if (l == null)
                throw new LineException("The line does not exist.");
            DataSource.Lines.Remove(l);
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
            Line l = DataSource.Lines.Find(item => item.ThisSerial == serial);
            if (l == null)
                return null;
            return l.Clone();
        }
        public IEnumerable<Line> GetLines()
        {
            return (from item in DataSource.Lines
                    select item.Clone()).OrderBy(item => item.NumberLine);
        }
        public IEnumerable<Line> GetLines(Predicate<Line> condition)
        {
            return (from item in DataSource.Lines
                    where condition(item)
                    select item.Clone()).OrderBy(item => item.NumberLine);           
        }
        public int countLines()
        {
            return DataSource.Lines.Count();
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
            try
            {
                DataSource.Stations.RemoveAll(item => item.ID == station.ID);
            }
            catch (ArgumentNullException)
            {
                throw new StationException("The station does not exist.");
            }
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
            Station station= DataSource.Stations.Find(item => item.ID == id);
            if (station == null)
                return null;
            return station.Clone();
        }
        public IEnumerable<Station> GetStations()
        {
            return (from item in DataSource.Stations
                    select item.Clone()).OrderBy(item => item.ID);
        }
        public IEnumerable<Station> GetStations(Predicate<Station> condition)
        {
            return (from item in DataSource.Stations
                    where condition(item)
                    select item.Clone()).OrderBy(item => item.ID); ;           
        }
        public int countStations()
        {
            return DataSource.Stations.Count();
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
            LineStation l = DataSource.LineStations.Find(item => item.NumberLine == lineStation.NumberLine && item.ID == lineStation.ID);
            if (l == null)
                throw new StationException("The line station does not exist.");
            DataSource.LineStations.Remove(l); // remove the old line station            
        }
        public void updateLineStation(LineStation lineStation)
        {
            removeLineStation(lineStation); // remove the old line station=delete according the key.
            DataSource.LineStations.Add(lineStation.Clone()); // add the updated line station
        }
        public LineStation getLineStation(int numberLine, int id)
        {
            LineStation lineStation= DataSource.LineStations.Find(item => item.NumberLine == numberLine && item.ID == id);
            if (lineStation == null)
                return null;
            return lineStation.Clone();
        }
        public IEnumerable<LineStation> GetLineStations()
        {
            return from item in DataSource.LineStations
                   select item.Clone();           
        }
        public IEnumerable<LineStation> GetLineStations(Predicate<LineStation> condition)
        {
            return (from item in DataSource.LineStations
                   where condition(item)
                   select item.Clone()).OrderBy(item => item.PathIndex);      
        }

        #endregion

        #region FollowingStations

        public void addTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {
            if(!DataSource.Stations.Exists(item => item.ID == twoFollowingStations.FirstStationID) || !DataSource.Stations.Exists(item => item.ID == twoFollowingStations.SecondStationID))
                throw new StationException("At least one of the station does not exist.");
            if (twoFollowingStations.FirstStationID == twoFollowingStations.SecondStationID)
                throw new StationException("Two identical stations.");
            if (DataSource.FollowingStations.Exists(item => (item.FirstStationID == twoFollowingStations.FirstStationID && item.SecondStationID == twoFollowingStations.SecondStationID) || (item.FirstStationID == twoFollowingStations.SecondStationID && item.SecondStationID == twoFollowingStations.FirstStationID)))
                throw new StationException("The two following stations already exist.");
            DataSource.FollowingStations.Add(twoFollowingStations.Clone());
        }
        public void removeTwoFollowingStations(TwoFollowingStations twoFollowingStations)
        {
            TwoFollowingStations f = DataSource.FollowingStations.Find(item => (item.FirstStationID == twoFollowingStations.FirstStationID && item.SecondStationID == twoFollowingStations.SecondStationID) || (item.FirstStationID == twoFollowingStations.SecondStationID && item.SecondStationID == twoFollowingStations.FirstStationID));
            if (f == null)
                throw new StationException("The two following stations do not exist.");
            DataSource.FollowingStations.Remove(f); // remove the old two following stations
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
            TwoFollowingStations twoFollowingStations= DataSource.FollowingStations.Find(item => (item.FirstStationID == firstStationID && item.SecondStationID == secondStationID) || (item.FirstStationID == secondStationID && item.SecondStationID == firstStationID));
            if (twoFollowingStations == null)
                return null;
            return twoFollowingStations.Clone();
        }
        public IEnumerable<TwoFollowingStations> GetFollowingStations()
        {
            return (from item in DataSource.FollowingStations
                   select item.Clone()).OrderBy(item => item.TimeBetweenStations);
        }
        public IEnumerable<TwoFollowingStations> GetFollowingStations(Predicate<TwoFollowingStations> condition)
        {
            return (from item in DataSource.FollowingStations
                   where condition(item)
                   select item.Clone()).OrderBy(item => item.TimeBetweenStations);       
        }

        #endregion      

        #region DrivingLines

        public void addDrivingLine(DrivingLine drivingLine)
        {

            if (getLine(drivingLine.NumberLine) == null)// check if the line exists
            {
                throw new TripException("The line not exists.");
            }
            if (DataSource.DrivingLines.Exists(item => item.NumberLine == drivingLine.NumberLine && item.Start == drivingLine.Start))
                throw new TripException("The driving line already exists.");
            DataSource.DrivingLines.Add(drivingLine.Clone());
        }
        public void removeDrivingLine(DrivingLine drivingLine)
        {
            DrivingLine d = DataSource.DrivingLines.Find(item => item.NumberLine == drivingLine.NumberLine && item.Start == drivingLine.Start);
            if (d == null)
                throw new TripException("The driving line does not exist.");
            DataSource.DrivingLines.Remove(d);
        }
        public void updateDrivingLine(DrivingLine drivingLine)
        {
            DrivingLine d = DataSource.DrivingLines.Find(item => item.NumberLine == drivingLine.NumberLine && item.Start == drivingLine.Start);
            if (d == null)
                throw new TripException("The driving line does not exist.");           
            DataSource.DrivingLines.Remove(d); // remove the old driving line
            DataSource.DrivingLines.Add(drivingLine.Clone()); // add the updated driving line  
        }
        public DrivingLine getDrivingLine(int numberLine, TimeSpan start)
        {
            DrivingLine drivingLine = DataSource.DrivingLines.Find(item => item.NumberLine == numberLine && item.Start == start);
            if (drivingLine == null)
                return null;
            return drivingLine.Clone();
        }
        public IEnumerable<DrivingLine> GetDrivingLines()
        {
            return from item in DataSource.DrivingLines
                   select item.Clone();
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
