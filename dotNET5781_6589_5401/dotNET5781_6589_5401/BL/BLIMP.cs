﻿using BLAPI;
using BO;
using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public sealed class BLIMP : IBL
    {
        readonly IDAL dal = DalFactory.GetDal();

        private Random rand = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Get time of trip that with the given distance
        /// Choose the valocity randomaly
        /// </summary>
        /// <param name="distance">distance of trip = meters</param>
        /// <returns>time of trip = minutes</returns>
        int calculateTime(int distance) { return (int)(distance / 1000.0 / rand.Next(30, 61) * 3600); } // valocity of 30-60 km per hour

        #region Singelton

        static readonly BLIMP instance = new BLIMP();
        public static BLIMP Instance => instance;
        static BLIMP() { }
        BLIMP() { }

        #endregion

        #region Users

        /// <summary>
        /// Func that converts user of BO to user of DO
        /// </summary>
        /// <param name="user">user of BO</param>
        /// <returns>user of DO</returns>
        DO.User convertToUserDO(BO.User user)
        {
            return new DO.User()
            {
                Username = user.Username,
                Password = user.Password,
                IsManager = user.IsManager
            };
        }
        /// <summary>
        /// Func that converts user of DO to user of BO
        /// </summary>
        /// <param name="user">user of DO</param>
        /// <returns>user of BO</returns>
        BO.User convertToUserBO(DO.User user)
        {
            return new BO.User()
            {
                Username = user.Username,
                Password = user.Password,
                IsManager = user.IsManager
            };
        }
        /// <summary>
        /// add new user
        /// </summary>
        /// <param name="user">user to add</param>
        public void addUser(BO.User user)
        {
            try
            {
                dal.addUser(convertToUserDO(user));
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Remove user
        /// </summary>
        /// <param name="user">user to remove</param>
        public void removeUser(BO.User user)
        {
            try
            {
                dal.removeUser(convertToUserDO(user));
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Update password of user (username is const)
        /// </summary>
        /// <param name="user">user with updated password</param>
        public void updateUser(BO.User user)
        {
            try
            {
                dal.updateUser(convertToUserDO(user));
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Return user accordingly the username. if it does not exist, return null.
        /// </summary>
        /// <param name="username">username of user that need return.</param>
        /// <returns>user or null (-defualt)</returns>
        public BO.User getUser(string username)
        {
            DO.User user = dal.getUser(username);
            if (user == null)
                return null;
            return convertToUserBO(user);
        }
        /// <summary>
        /// get all users
        /// </summary>
        /// <returns>users</returns>
        public IEnumerable<BO.User> GetUsers()
        {
            return from user in dal.GetUsers()
                   select convertToUserBO(user);
        }
        /// <summary>
        /// get all users by condition
        /// </summary>
        /// <param name="condition">condition - Predicate</param>
        /// <returns>users</returns>
        public IEnumerable<BO.User> GetUsers(Predicate<BO.User> condition)
        {
            return from item in GetUsers()
                   where condition(item)
                   select item;
        }

        #endregion

        #region Buses

        /// <summary>
        /// Func that converts bus of BO to bus of DO
        /// </summary>
        /// <param name="bus">bus of BO</param>
        /// <returns>bus of DO</returns>
        DO.Bus convertToBusDO(BO.Bus bus)
        {
            return new DO.Bus()
            {
                LicensePlate = bus.LicensePlate,
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms,
                KmsSinceFuel = bus.KmsSinceFuel,
                KmsSinceService = bus.KmsSinceService,
                LastService = bus.LastService
            };
        }
        /// <summary>
        /// Func that converts bus of DO to bus of BO
        /// </summary>
        /// <param name="bus">bus of DO</param>
        /// <returns>bus of BO</returns>
        BO.Bus convertToBusBO(DO.Bus bus)
        {
            BO.Bus busB = new BO.Bus()
            {
                StartOfWork = bus.StartOfWork,
                LastService = bus.LastService,
                LicensePlate = bus.LicensePlate,
                TotalKms = bus.TotalKms,
                KmsSinceFuel = bus.KmsSinceFuel,
                KmsSinceService = bus.KmsSinceService
            };
            busB.Status = setState(busB);
            return busB;
        }
        /// <summary>
        /// set the bus status: canDrive, cannotDrive, driving, gettingFueled, gettingTreated
        /// </summary>
        /// <returns>bus status</returns>
        public BO.State setState(BO.Bus bus)
        {
            TimeSpan timeSinceLastTreat = DateTime.Now - bus.LastService;
            if (timeSinceLastTreat.TotalDays >= 365 || bus.KmsSinceService >= 20000 || bus.KmsSinceFuel >= 1200)
                return BO.State.cannotDrive;
            return BO.State.canDrive;
        }
        /// <summary>
        /// Add bus for the data.
        /// </summary>
        /// <param name="bus">Bus for add</param>
        public void addBus(BO.Bus bus)
        {
            try
            {
                dal.addBus(convertToBusDO(bus));
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Remove bus from the data.
        /// </summary>
        /// <param name="bus">Bus for remove</param>
        public void removeBus(BO.Bus bus)
        {
            try
            {
                dal.removeBus(convertToBusDO(bus));
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Update exsits bus.
        /// </summary>
        /// <param name="bus">Bus with updated part</param>
        public void updateBus(BO.Bus bus)
        {
            try
            {
                dal.updateBus(convertToBusDO(bus));
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Return bus according to the key of bus= license plate.
        /// </summary>
        /// <param name="licensePlate">key of the bus.</param>
        /// <returns>bus with this license</returns>
        public BO.Bus getBus(string licensePlate)
        {
            try
            {
                return convertToBusBO(dal.getBus(licensePlate));
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Return all the bus from the data
        /// </summary>
        /// <returns>list of buses</returns>
        public IEnumerable<BO.Bus> GetBuses()
        {
            try
            {
                return from bus in dal.GetBuses()
                       select convertToBusBO(bus);
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message, ex);
            }
        }
        /// <summary>
        /// return any bus that meets the condition.
        /// </summary>
        /// <param name="condition">about buses</param>
        /// <returns>list of buses with the condition.</returns>
        public IEnumerable<BO.Bus> GetBuses(Predicate<BO.Bus> condition)
        {
            try
            {
                return from item in GetBuses()
                       where condition(item)
                       select item;
            }
            catch (BO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        /// <summary>
        /// fuel the bus
        /// </summary>
        /// <param name="bus">bus for fueling</param>
        public void fuelBus(BO.Bus bus)
        {
            try
            {
                bus = getBus(bus.LicensePlate);
                bus.KmsSinceFuel = 0;
                dal.updateBus(convertToBusDO(bus));
            }
            catch (BO.BusException ex) // the bus does not exist
            {
                throw new BO.BusException(ex.Message);
            }
        }
        /// <summary>
        /// Service the bus, and fuel too when needed
        /// </summary>
        /// <param name="bus">bus for service</param>
        public void serviceBus(BO.Bus bus)
        {
            try
            {
                bus = getBus(bus.LicensePlate);
                bus.KmsSinceService = 0;
                bus.LastService = DateTime.Now.Date;
                if (bus.KmsSinceFuel > 1100) // needs refueling soon
                    bus.KmsSinceFuel = 0;
                dal.updateBus(convertToBusDO(bus));
            }
            catch (BO.BusException ex) // the bus does not exist
            {
                throw new BO.BusException(ex.Message);
            }
        }

        #endregion

        #region Lines

        /// <summary>
        /// Func that converts line of BO to line of DO
        /// </summary>
        /// <param name="bus">line of BO</param>
        /// <returns>line of DO</returns>
        DO.Line convertToLineDO(BO.Line lineB)
        {
            return new DO.Line()
            {
                ThisSerial = lineB.ThisSerial,
                NumberLine = lineB.NumberLine,
                Region = (DO.Regions)lineB.Region
            };
        }
        /// <summary>
        /// Func that converts line of DO to line of BO
        /// </summary>
        /// <param name="bus">line of DO</param>
        /// <returns>line of BO</returns>
        BO.Line convertToLineBO(DO.Line lineD)
        {
            return new BO.Line()
            {
                ThisSerial = lineD.ThisSerial,
                NumberLine = lineD.NumberLine,
                Region = (BO.Regions)lineD.Region,
                Path = GetLineStations(lineStation => lineStation.NumberLine == lineD.ThisSerial).OrderBy(station => station.PathIndex)
            };
        }
        /// <summary>
        /// Add line to the data.
        /// </summary>
        /// <param name="line">line to add</param>
        public void addLine(BO.Line line)
        {
            try
            {
                if (line.Path.Count() < 2)
                    throw new BO.LineException("A line path should contain at least two stations.");

                int thisSerial = dal.addLine(convertToLineDO(line));
                foreach (BO.LineStation lineStation in line.Path)
                {
                    lineStation.NumberLine = thisSerial;
                    addLineStation(lineStation);
                }
            }
            catch (BO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Remove line from the data
        /// </summary>
        /// <param name="line">Removing</param>
        public void removeLine(BO.Line line)
        {
            try
            {
                dal.removeLine(convertToLineDO(line));
                foreach (DO.LineStation lineStation in dal.GetLineStations(item => item.NumberLine == line.ThisSerial).ToList())
                    dal.removeLineStation(lineStation);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Update data of line.
        /// </summary>
        /// <param name="line">Updated line</param>
        public void updateLine(BO.Line line)
        {
            try
            {
                dal.updateLine(convertToLineDO(line));

                // update line stations:
                foreach (DO.LineStation lineStation in dal.GetLineStations(item => item.NumberLine == line.ThisSerial).ToList())
                    dal.removeLineStation(lineStation);
                foreach (BO.LineStation lineStation in line.Path)
                {
                    lineStation.NumberLine = line.ThisSerial;
                    addLineStation(lineStation);
                }
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Return line according it key = the serial.
        /// </summary>
        /// <param name="serial">Key of line.</param>
        /// <returns>Line with this serial.</returns>
        public BO.Line getLine(int serial)
        {
            try
            {
                return convertToLineBO(dal.getLine(serial));
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Return all the saved lines.
        /// </summary>
        /// <returns>List of lines.</returns>
        public IEnumerable<BO.Line> GetLines()
        {
            try
            {
                return from line in dal.GetLines()
                       orderby line.ThisSerial
                       select convertToLineBO(line);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Return line accordingly the condition.
        /// </summary>
        /// <param name="condition">Delegate of Predicate, return 'true' or 'false' about any line</param>
        /// <returns>list of specific line.</returns>
        public IEnumerable<BO.Line> GetLines(Predicate<BO.Line> condition)
        {
            try
            {
                return from item in GetLines()
                       where condition(item)
                       select item;
            }
            catch (BO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }
        /// <summary>
        /// get the last station in the line path
        /// </summary>
        /// <param name="serial"></param>
        /// <returns></returns>
        public int getLastStation(int serial)
        {
            try
            {
                return getLine(serial).Path.Last().ID;
            }
            catch (BO.LineException ex)
            {
                throw new BO.LineException(ex.Message, ex);
            }
        }
        /// <summary>
        /// group lines by region
        /// </summary>
        /// <returns>grouping</returns>
        public IEnumerable<IGrouping<BO.Regions, int>> GetLinesByRegion()
        {
            return from line in dal.GetLines()
                   group line.ThisSerial by (BO.Regions)line.Region;
        }
        /// <summary>
        /// impossible to change a line if it is driving
        /// </summary>
        /// <param name="line">line serial number</param>
        /// <returns>can or cannot</returns>
        public bool canChangeLine(int line)
        {
            foreach (DO.DrivingLine drivingLine in dal.GetDrivingLines(item => item.NumberLine == line))
                if (drivingLine.Start <= DateTime.Now.TimeOfDay && drivingLine.End + duration(getLine(line).Path).SecondsToTimeSpan() >= DateTime.Now.TimeOfDay)
                    return false;
            return true;
        }
        /// <summary>
        /// Get number of lines in the data base
        /// </summary>
        /// <returns>count of lines</returns>
        public int countLines()
        {
            return dal.countLines();
        }

        #endregion

        #region Stations

        /// <summary>
        /// Func that converts station of BO to station of DO
        /// </summary>
        /// <param name="stationB">station of BO</param>
        /// <returns>station of DO</returns>
        DO.Station convertToStationDO(BO.Station stationB)
        {
            return new DO.Station()
            {
                ID = stationB.ID,
                Name = stationB.Name,
                Latitude = stationB.Latitude,
                Longitude = stationB.Longitude
            };
        }
        /// <summary>
        /// Func that converts station of DO to station of BO
        /// </summary>
        /// <param name="stationD">station of DO</param>
        /// <returns>station of BO</returns>
        BO.Station convertToStationBO(DO.Station stationD)
        {
            try
            {
                return new BO.Station()
                {
                    ID = stationD.ID,
                    Name = stationD.Name,
                    Latitude = stationD.Latitude,
                    Longitude = stationD.Longitude,
                    LinesAtStation = GetLineStations(lineStation => lineStation.ID == stationD.ID).OrderBy(station => station.ID)
                };
            }
            catch (BO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        /// <summary>
        /// Take data from statin about following stations.
        /// </summary>
        /// <param name="station">data about station.</param>
        void stationToFollowingStation(BO.Station station, int distanceFromPreviousLocation)
        {
            IEnumerable<BO.Station> stations = GetStations(item => item.ID != station.ID);
            if (stations != null)
                foreach (BO.Station otherStation in stations)
                    try
                    {
                        TwoFollowingStations followingStations = dal.getTwoFollowingStations(station.ID, otherStation.ID);
                        if (followingStations == null)
                            break;
                        followingStations.LengthBetweenStations = Math.Abs(followingStations.LengthBetweenStations + distanceFromPreviousLocation); // avoid negetive length
                        followingStations.TimeBetweenStations = calculateTime(followingStations.LengthBetweenStations);
                        dal.updateTwoFollowingStations(followingStations);
                    }
                    catch (DO.StationException) { }
        }
        /// <summary>
        /// Add station to the saved data.
        /// </summary>
        /// <param name="station">Station for add.</param>
        public void addStation(BO.Station station)
        {
            try
            {
                dal.addStation(convertToStationDO(station));
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
        }
        /// <summary>
        /// remove station from the data.
        /// </summary>
        /// <param name="station">Station for remove</param>
        public void removeStation(BO.Station station)
        {
            try
            {
                dal.removeStation(convertToStationDO(station));
                foreach (BO.LineStation lineStation in GetLineStations(item => item.ID == station.ID).ToList())
                    removeLineStation(lineStation);
                foreach (TwoFollowingStations followingStations in dal.GetFollowingStations(item => item.FirstStationID == station.ID || item.SecondStationID == station.ID).ToList())
                    dal.removeTwoFollowingStations(followingStations);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
            catch (BO.StationException ex)
            {
                throw new BO.StationException(ex.Message);

            }
        }
        /// <summary>
        /// update data about station.
        /// </summary>
        /// <param name="station">Updated station</param>
        public void updateStation(BO.Station station, int distanceFromPreviousLocation)
        {
            try
            {
                dal.updateStation(convertToStationDO(station));
                if (distanceFromPreviousLocation != 0)
                    stationToFollowingStation(station, distanceFromPreviousLocation);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Return station with this key
        /// </summary>
        /// <param name="id">key</param>
        /// <returns>station</returns>
        public BO.Station getStation(int id)
        {
            try
            {
                return convertToStationBO(dal.getStation(id));
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
        }
        /// <summary>
        /// return all the stations.
        /// </summary>
        /// <returns>list of all stations.</returns>
        public IEnumerable<BO.Station> GetStations()
        {
            try
            {
                return from station in dal.GetStations()
                       select convertToStationBO(station);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Return station that for these the Predicate return true.
        /// </summary>
        /// <param name="condition">Predicete' condition about station</param>
        /// <returns>list of specific stations</returns>
        public IEnumerable<BO.Station> GetStations(Predicate<BO.Station> condition)
        {
            try
            {
                return from item in GetStations()
                       where condition(item)
                       select item;
            }
            catch (BO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        /// <summary>
        /// impossible to change a station if there are driving lines that stop there
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool canChangeStation(BO.Station station)
        {
            foreach (DO.LineStation lineStation in dal.GetLineStations(item => item.ID == station.ID))
                foreach (DO.DrivingLine drivingLine in dal.GetDrivingLines(item => item.NumberLine == lineStation.NumberLine))
                    if (drivingLine.Start <= DateTime.Now.TimeOfDay && drivingLine.End + duration(getLine(lineStation.NumberLine).Path).SecondsToTimeSpan() >= DateTime.Now.TimeOfDay)
                        return false;
            return true;
        }
        /// <summary>
        /// Get number of stations in the data base
        /// </summary>
        /// <returns>count</returns>
        public int countStations()
        {
            return dal.countStations();
        }

        #endregion

        #region LineStations

        /// <summary>
        /// Func that converts line station of BO to line station of DO
        /// </summary>
        /// <param name="lineStationB">line station of BO</param>
        /// <returns>line station of DO</returns>
        DO.LineStation convertToLineStationDO(BO.LineStation lineStationB)
        {
            return new DO.LineStation()
            {
                ID = lineStationB.ID,
                NumberLine = lineStationB.NumberLine,
                PathIndex = lineStationB.PathIndex
            };
        }
        /// <summary>
        /// Func that converts line station of DO to line station of BO
        /// </summary>
        /// <param name="lineStationD">line station of DO</param>
        /// <returns>line station of BO</returns>
        BO.LineStation convertToLineStationBO(DO.LineStation lineStationD)
        {
            BO.LineStation lineStationB = new BO.LineStation()
            {
                NumberLine = lineStationD.NumberLine,
                ID = lineStationD.ID,
                PathIndex = lineStationD.PathIndex
            };

            // previous:
            DO.LineStation lineStationP = dal.GetLineStations(item => item.NumberLine == lineStationD.NumberLine && item.PathIndex == lineStationD.PathIndex - 1).FirstOrDefault();
            if (lineStationP != null)
            {
                TwoFollowingStations followingStations = dal.getTwoFollowingStations(lineStationB.ID, lineStationP.ID);
                lineStationB.PreviousStationID = lineStationP.ID;
                lineStationB.LengthFromPreviousStations = followingStations.LengthBetweenStations;
                lineStationB.TimeFromPreviousStations = followingStations.TimeBetweenStations;
            }
            else // if it is the first line station
            {
                lineStationB.PreviousStationID = -1;
                lineStationB.LengthFromPreviousStations = -1;
                lineStationB.TimeFromPreviousStations = -1;
            }

            // next:
            DO.LineStation lineStationN = dal.GetLineStations(item => item.NumberLine == lineStationD.NumberLine && item.PathIndex == lineStationD.PathIndex + 1).FirstOrDefault();
            if (lineStationN != null)
                lineStationB.NextStationID = lineStationN.ID;
            else // if it is the last line station
                lineStationB.NextStationID = -1;

            return lineStationB;
        }
        /// <summary>
        /// Convert IEnumerable of Station to IEnumerable of LineStation in order to create a line path in PL from ObservableCollection of Station
        /// </summary>
        /// <param name="path">ObservableCollection of Station</param>
        /// <returns>ObservableCollection of Station</returns>
        public IEnumerable<BO.LineStation> convertToLineStationsList(IEnumerable<BO.Station> path)
        {
            List<BO.Station> pathTemp = path.ToList();
            List<BO.LineStation> lineStations = new List<BO.LineStation>();
            TwoFollowingStations followingStations;

            lineStations.Add(new BO.LineStation() // first station
            {
                ID = pathTemp[0].ID,
                PathIndex = 1,
                NextStationID = pathTemp[1].ID,
                PreviousStationID = -1,
                LengthFromPreviousStations = -1,
                TimeFromPreviousStations = -1
            });

            for (int i = 1; i < pathTemp.Count() - 1; i++)
            {
                followingStations = dal.getTwoFollowingStations(pathTemp[i].ID, pathTemp[i - 1].ID);
                lineStations.Add(new BO.LineStation()
                {
                    ID = pathTemp[i].ID,
                    PathIndex = i + 1,
                    NextStationID = pathTemp[i + 1].ID,
                    PreviousStationID = pathTemp[i - 1].ID,
                    LengthFromPreviousStations = followingStations.LengthBetweenStations,
                    TimeFromPreviousStations = followingStations.TimeBetweenStations
                });
            }

            followingStations = dal.getTwoFollowingStations(pathTemp[pathTemp.Count() - 1].ID, pathTemp[path.Count() - 2].ID);
            lineStations.Add(new BO.LineStation() // last station
            {

                ID = pathTemp[pathTemp.Count() - 1].ID,
                PathIndex = pathTemp.Count(),
                NextStationID = -1,
                PreviousStationID = pathTemp[pathTemp.Count() - 2].ID,
                LengthFromPreviousStations = followingStations.LengthBetweenStations,
                TimeFromPreviousStations = followingStations.TimeBetweenStations
            }); ;

            return lineStations;
        }
        /// <summary>
        /// Convert IEnumerable of LineStation to IEnumerable of Station in order to copy the line path into ObservableCollection of Station in PL
        /// </summary>
        /// <param name="path">IEnumerable of LineStation</param>
        /// <returns>IEnumerable of Station</returns>
        public IEnumerable<BO.Station> convertToStationsList(IEnumerable<BO.LineStation> path)
        {
            return from station in path
                   select getStation(station.ID);
        }
        /// <summary>
        /// Add station in line
        /// </summary>
        /// <param name="lineStation">station in line</param>
        public void addLineStation(BO.LineStation lineStation)
        {
            try
            {
                dal.addLineStation(convertToLineStationDO(lineStation));

                // update index of the next stations in the path:
                List<BO.LineStation> lineStations = GetLineStations(station => station.NumberLine == lineStation.NumberLine && station.ID != lineStation.ID).OrderBy(item => item.PathIndex).ToList(); // other stations in the path
                for (int i = lineStation.PathIndex - 1; i < lineStations.Count(); i++)
                {
                    lineStations[i].PathIndex++;
                    dal.updateLineStation(convertToLineStationDO(lineStations[i]));
                }
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Remove station of line
        /// </summary>
        /// <param name="lineStation">station in line</param>
        public void removeLineStation(BO.LineStation lineStation)
        {
            try
            {
                dal.removeLineStation(convertToLineStationDO(lineStation));

                List<BO.LineStation> lineStations = GetLineStations(Station => Station.NumberLine == lineStation.NumberLine).OrderBy(station => station.PathIndex).ToList();

                // if there is only one more line station in this line, delete the line:
                if (lineStations.Count() == 1)
                {
                    dal.removeLine(dal.getLine(lineStation.NumberLine));
                    dal.removeLineStation(convertToLineStationDO(lineStations[0]));
                    return;
                }

                // update index of the next stations in the path:
                for (int i = lineStation.PathIndex - 1; i < lineStations.Count(); i++)
                {
                    lineStations[i].PathIndex--;
                    dal.updateLineStation(convertToLineStationDO(lineStations[i]));
                }
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Return station in line with the keys: numberLine, id.
        /// </summary>
        /// <param name="numberLine">the line that the station in it path.</param>
        /// <param name="id">number of the station</param>
        /// <returns>station in line</returns>
        public BO.LineStation getLineStation(int numberLine, int id)
        {
            DO.LineStation lineStation = dal.getLineStation(numberLine, id);
            if (lineStation == null)
                return null;
            return convertToLineStationBO(lineStation);

        }
        /// <summary>
        /// get all line stations
        /// </summary>
        /// <returns>line stations</returns>
        public IEnumerable<BO.LineStation> GetLineStations()
        {
            try
            {
                return from lineStation in dal.GetLineStations()
                       select convertToLineStationBO(lineStation);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Get line stations by condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>line stations</returns>
        public IEnumerable<BO.LineStation> GetLineStations(Predicate<BO.LineStation> condition)
        {
            try
            {
                return from item in GetLineStations()
                       where condition(item)
                       select item;
            }
            catch (BO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }

        #endregion

        #region TwoFollowingStations

        /// <summary>
        /// Add two following stations
        /// </summary>
        /// <param name="firstID">first station ID</param>
        /// <param name="secondID">second station ID</param>
        /// <param name="length">length</param>
        public void addTwoFollowingStations(int firstID, int secondID, int length)
        {
            try
            {
                dal.addTwoFollowingStations(new TwoFollowingStations()
                {
                    FirstStationID = firstID,
                    SecondStationID = secondID,
                    LengthBetweenStations = length,
                    TimeBetweenStations = calculateTime(length)
                });
            }
            catch (DO.StationException ex) { throw new BO.StationException(ex.Message, ex); }
        }
        /// <summary>
        /// Test if TwoFollowingStations exists for two stations
        /// </summary>
        /// <param name="firstID">first station ID</param>
        /// <param name="secondID">second station ID</param>
        /// <returns>exists or not</returns>
        public bool getTwoFollowingStations(int firstID, int secondID)
        {
            if (dal.getTwoFollowingStations(firstID, secondID) == null)
                return false;
            return true;
        }

        #endregion

        #region DrivingBuses

        /// <summary>
        /// Get a spicific trip identified by its line serial number of the start time of the trip
        /// </summary>
        /// <param name="serial">line serial number</param>
        /// <param name="start">start time of trip</param>
        /// <returns>trip</returns>
        private DrivingBus getTrip(int serial, DateTime start)
        {
            try
            {
                List<BO.LineStation> path = getLine(serial).Path.ToList();
                int previousStationTime;
                int index = getPreviousStationIndex(path, DateTime.Now - start, out previousStationTime);
                if (index == 0)
                    return null;

                return new DrivingBus()
                {
                    NumberLine = serial,
                    Start = start,
                    PreviousStationID = path[index - 1].ID,
                    PreviousStationTime = previousStationTime.SecondsToTimeSpan(),
                    NextStationTime = (path[index].TimeFromPreviousStations - previousStationTime).SecondsToTimeSpan()
                };
            }
            catch (BO.LineException ex)
            {
                throw new BO.TripException(ex.Message, ex);
            }

        }
        /// <summary>
        /// Get all existing trips
        /// </summary>
        /// <returns>trips</returns>
        private IEnumerable<DrivingBus> GetTrips()
        {
            try
            {
                List<DO.DrivingLine> drivingLines = dal.GetDrivingLines().ToList();
                if (drivingLines.Count() == 0)
                    throw new BO.TripException("No trips exist.");

                List<DrivingBus> trips = new List<DrivingBus>();
                foreach (DO.DrivingLine drivingLine in drivingLines)
                    for (TimeSpan i = drivingLine.Start; i < drivingLine.End; i += drivingLine.Frequency.MinutesToTimeSpan())
                    {
                        DrivingBus trip = getTrip(drivingLine.NumberLine, i.ToDateTime());
                        if (trip != null)
                            trips.Add(trip);
                    }
                return trips;
            }
            catch (BO.TripException ex)
            {
                throw new BO.TripException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Get all trips that a passenger may use to arrive from souece station to a target station
        /// Those trips may occur now or in the future
        /// If there are more than 10 present trips, do not search in the future
        /// </summary>
        /// <param name="source">source station ID</param>
        /// <param name="target">target station ID</param>
        /// <returns>possible trips for the passenger</returns>
        public IEnumerable<DrivingBus> getPassengerTrips(int source, int target)
        {
            List<BO.Line> lines = new List<BO.Line>();

            foreach (DO.Line line in dal.GetLines().ToList())
            {
                DO.LineStation tSource = dal.getLineStation(line.ThisSerial, source);
                DO.LineStation tTarget = dal.getLineStation(line.ThisSerial, target);
                if (tSource != null && tTarget != null && tTarget.PathIndex > tSource.PathIndex)
                    lines.Add(getLine(line.ThisSerial));
            }

            List<DrivingBus> allTrips = new List<DrivingBus>();
            foreach (BO.Line line in lines)
            {
                IEnumerable<DrivingBus> present = GetTripsOfLine_Present(line.ThisSerial);
                if (present != null)
                    foreach (DrivingBus trip in present)
                        if (trip.PreviousStationID == -1 || dal.getLineStation(line.ThisSerial, trip.PreviousStationID).PathIndex < dal.getLineStation(line.ThisSerial, source).PathIndex)
                            allTrips.Add(trip);
            }

            if (allTrips.Count() < 10)
                foreach (BO.Line line in lines)
                {
                    IEnumerable<DrivingBus> future = GetTripsOfLine_Future(line.ThisSerial);
                    if (future != null)
                        foreach (DrivingBus trip in future)
                            allTrips.Add(trip);
                }

            return allTrips;
        }
        /// <summary>
        /// Get all trips of the given line that occur at this moment
        /// </summary>
        /// <param name="serial">line serial number</param>
        /// <returns>present trips</returns>
        private IEnumerable<DrivingBus> GetTripsOfLine_Present(int serial)
        {
            List<DO.DrivingLine> drivingLines = dal.GetDrivingLines(item => item.NumberLine == serial).ToList();
            if (drivingLines.Count() == 0)
                return null;

            List<DrivingBus> trips = new List<DrivingBus>();
            foreach (DO.DrivingLine drivingLine in drivingLines)
                for (TimeSpan i = drivingLine.Start; i < DateTime.Now.TimeOfDay && i < drivingLine.End; i += drivingLine.Frequency.MinutesToTimeSpan())
                {
                    DrivingBus trip = getTrip(serial, i.ToDateTime());
                    if (trip != null)
                        trips.Add(trip);
                }
            return trips;
        }
        /// <summary>
        /// Get all trips of the given line that will occur from the next closest trip till the end of the day
        /// </summary>
        /// <param name="serial">line serial number</param>
        /// <returns>future trips</returns>
        private IEnumerable<DrivingBus> GetTripsOfLine_Future(int serial)
        {
            List<DO.DrivingLine> drivingLines = dal.GetDrivingLines(item => item.NumberLine == serial).ToList();
            if (drivingLines.Count() == 0)
                return null;

            List<DrivingBus> trips = new List<DrivingBus>();
            foreach (DO.DrivingLine drivingLine in drivingLines)
                for (TimeSpan i = getClosestStart(drivingLine.Start, drivingLine.End, drivingLine.Frequency); i < drivingLine.End; i += drivingLine.Frequency.MinutesToTimeSpan())
                    trips.Add(new DrivingBus()
                    {
                        NumberLine = drivingLine.NumberLine,
                        Start = i.ToDateTime(),
                        PreviousStationID = -1,
                        PreviousStationTime = new TimeSpan(0),
                        NextStationTime = i - DateTime.Now.TimeOfDay
                    });
            return trips;
        }
        /// <summary>
        /// Calculate the time from this moment till the given line will arrive at the given station
        /// The trip may already began or not
        /// </summary>
        /// <param name="trip">line trip</param>
        /// <param name="source">source station</param>
        /// <returns>time till arrival to source</returns>
        public TimeSpan timeTillArrivalToSource(DrivingBus trip, int source)
        {
            DO.LineStation sourceStation = dal.getLineStation(trip.NumberLine, source);
            DO.LineStation previousStation = new DO.LineStation();

            if (trip.PreviousStationID != -1)
            {
                previousStation = dal.getLineStation(trip.NumberLine, trip.PreviousStationID);
                if (previousStation.PathIndex > sourceStation.PathIndex)
                    return new TimeSpan(-1, -1, -1); // if it passed the source station
            }

            TimeSpan time = trip.NextStationTime;
            int timeSec = 0;
            int index = 1;
            if (trip.PreviousStationID != -1)
                index = previousStation.PathIndex + 1;
            List<BO.LineStation> path = getLine(trip.NumberLine).Path.ToList();
            for (int i = index; i < sourceStation.PathIndex; i++)
                timeSec += path[index].TimeFromPreviousStations;
            return time + timeSec.SecondsToTimeSpan();
        }
        /// <summary>
        /// Calculate time of journey from one station to another
        /// </summary>
        /// <param name="serial">line serial number</param>
        /// <param name="source">source station</param>
        /// <param name="target">target station</param>
        /// <returns>time of journey</returns>
        public TimeSpan durationTripBetweenStations(int serial, int source, int target)
        {
            int duration = 0;
            List<BO.LineStation> path = getLine(serial).Path.ToList();
            int finalIndex = dal.getLineStation(serial, target).PathIndex;
            for (int i = dal.getLineStation(serial, source).PathIndex; i < finalIndex; i++)
                duration += path[i].TimeFromPreviousStations;
            return duration.SecondsToTimeSpan();
        }
        /// <summary>
        /// Calculate time of journey of the given line path
        /// </summary>
        /// <param name="path"></param>
        /// <returns>minutes of duration</returns>
        public int duration(IEnumerable<BO.LineStation> path)
        {
            int duration = 1; // because time to previous of the first is -1
            foreach (BO.LineStation lineStation in path)
                duration += lineStation.TimeFromPreviousStations;
            return duration;
        }
        /// <summary>
        /// Get previous station index and time
        /// </summary>
        /// <param name="path">line path</param>
        /// <param name="timeOfTrip">duration</param>
        /// <param name="previousStationTime">out parameter calculated during the method</param>
        /// <returns>previous station index</returns>
        private int getPreviousStationIndex(IEnumerable<BO.LineStation> path, TimeSpan timeOfTrip, out int previousStationTime)
        {
            previousStationTime = 0;
            int secondsOfTrip = (int)timeOfTrip.TotalSeconds;
            if (duration(path) < secondsOfTrip)
                return 0; // not found
            int time = 1; // because time to previous of the first is -1
            foreach (BO.LineStation lineStation in path)
            {
                time += lineStation.TimeFromPreviousStations;
                if (time >= secondsOfTrip)
                {
                    previousStationTime = lineStation.TimeFromPreviousStations - (time - secondsOfTrip);
                    return lineStation.PathIndex - 1; // the path index (which starts at 1) of previous station
                }
            }
            return 0; // not found
        }
        /// <summary>
        /// Get the next time some line will go for a trip
        /// </summary>
        /// <param name="start">start time of range</param>
        /// <param name="end">end time of range</param>
        /// <param name="frequency">frequency of trips</param>
        /// <returns>next start time</returns>
        private TimeSpan getClosestStart(TimeSpan start, TimeSpan end, int frequency)
        {
            for (TimeSpan closestStart = start; closestStart <= end; closestStart += frequency.MinutesToTimeSpan())
                if (closestStart >= DateTime.Now.TimeOfDay)
                    return closestStart;
            return end;
        }

        #endregion

        #region DrivingLines

        /// <summary>
        /// Func that converts driving line of BO to driving line of DO
        /// </summary>
        /// <param name="drivingLine">driving line of BO</param>
        /// <returns>driving line of DO</returns>
        DO.DrivingLine convertToDrivingLineDO(BO.DrivingLine drivingLine)
        {
            return new DO.DrivingLine()
            {
                NumberLine = drivingLine.NumberLine,
                Start = drivingLine.Start,
                End = drivingLine.End,
                Frequency = drivingLine.Frequency
            };
        }
        /// <summary>
        /// Func that converts driving line of DO to driving line of BO
        /// </summary>
        /// <param name="drivingLine">driving line of DO</param>
        /// <returns>driving line of BO</returns>
        BO.DrivingLine convertToDrivingLineBO(DO.DrivingLine drivingLine)
        {
            return new BO.DrivingLine()
            {
                NumberLine = drivingLine.NumberLine,
                Start = drivingLine.Start,
                Frequency = drivingLine.Frequency,
                End = drivingLine.End
            };
        }
        /// <summary>
        /// Add a new drivingLine
        /// Make sure its range of time does not overlap any other drivingLine of this line
        /// </summary>
        /// <param name="drivingLine"></param>
        public void addDrivingLine(BO.DrivingLine drivingLine)
        {
            try
            {
                foreach (BO.DrivingLine dLine in GetDrivingLines(item => item.NumberLine == drivingLine.NumberLine))
                    if ((drivingLine.Start >= dLine.Start && drivingLine.Start <= dLine.End) || (drivingLine.End > dLine.Start && drivingLine.End < dLine.End) || (drivingLine.Start <= dLine.Start && drivingLine.End >= dLine.End))
                        throw new BO.TripException("A trip cannot overlap another trip of the line.");

                dal.addDrivingLine(convertToDrivingLineDO(drivingLine));
            }
            catch (DO.TripException ex)
            {
                throw new BO.TripException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Remove a drivingLine
        /// </summary>
        /// <param name="drivingLine">drivingLine to remove</param>
        public void removeDrivingLine(BO.DrivingLine drivingLine)
        {
            try
            {
                dal.removeDrivingLine(convertToDrivingLineDO(drivingLine));
            }
            catch (DO.TripException ex)
            {
                throw new BO.TripException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Update a drivingLine
        /// </summary>
        /// <param name="drivingLine">drivingLine to update</param>
        public void updateDrivingLine(BO.DrivingLine drivingLine)
        {
            try
            {
                dal.updateDrivingLine(convertToDrivingLineDO(drivingLine));
            }
            catch (DO.TripException ex)
            {
                throw new BO.TripException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Get specific drivingLine
        /// </summary>
        /// <param name="numberLine">line serial number</param>
        /// <param name="start">time of start</param>
        /// <returns>trip</returns>
        public BO.DrivingLine getDrivingLine(int numberLine, TimeSpan start)
        {

            DO.DrivingLine drivingLine = dal.getDrivingLine(numberLine, start);
            if (drivingLine == null)
                return null;
            return convertToDrivingLineBO(drivingLine);
        }
        /// <summary>
        /// Get all existing driving lines
        /// </summary>
        /// <returns>driving lines</returns>
        public IEnumerable<BO.DrivingLine> GetDrivingLines()
        {
            return from drivingLine in dal.GetDrivingLines()
                   select convertToDrivingLineBO(drivingLine);
        }
        /// <summary>
        /// Get driving lines by condition
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns>driving lines</returns>
        public IEnumerable<BO.DrivingLine> GetDrivingLines(Predicate<BO.DrivingLine> condition)
        {
            return from item in GetDrivingLines()
                   where condition(item)
                   orderby item.Start
                   select item;
        }
        /// <summary>
        /// Get start times of the given line
        /// </summary>
        /// <param name="numberLine">serial number of line</param>
        /// <returns>list of start times</returns>
        public IEnumerable<TimeSpan> getTripsStart(int numberLine)
        {
            List<TimeSpan> tripsStart = new List<TimeSpan>();
            foreach (BO.DrivingLine drivingLine in GetDrivingLines(item => item.NumberLine == numberLine))
                if (drivingLine.Frequency == 0) // one trip
                    tripsStart.Add(drivingLine.Start);
                else // multiple trips
                    for (TimeSpan i = drivingLine.Start; i <= drivingLine.End; i += drivingLine.Frequency.MinutesToTimeSpan())
                        tripsStart.Add(i);
            return tripsStart;
        }

        #endregion

        #region ManagingCode

        /// <summary>
        /// Get managing code
        /// </summary>
        /// <returns>managing code</returns>
        public string getManagingCode()
        {
            return dal.getManagingCode();
        }
        /// <summary>
        /// Set managing code
        /// </summary>
        /// <param name="code">updated managing code</param>
        public void updateManagingCode(string code)
        {
            dal.updateManagingCode(code);
        }

        #endregion
    }
}