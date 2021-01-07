using System;
using System.Collections.Generic;
using System.Linq;
using System.Device.Location;
using BLAPI;
using DLAPI;
using DO;

namespace BL
{
    public sealed class BLIMP : IBL
    {
        readonly IDAL dal = DalFactory.GetDal();

        private Random rand = new Random(DateTime.Now.Millisecond);

        #region Singelton

        //static readonly BLIMP instance = new BLIMP();
        //public static BLIMP Instance => instance;
        //static BLIMP() { }
        //BLIMP() { }

        #endregion

        #region Help functions
        int calculateTime(int distance)
        {
            return (int)((distance/1000.0/rand.Next(30,61))*3600); // valocity of 30-60 km per hour
        }
        void addOrUpdateTwoFollowingStations(TwoFollowingStations followingStations)
        {
            try
            {
                dal.addTwoFollowingStations(followingStations);
            }
            catch (StationException)
            {
                dal.updateTwoFollowingStations(followingStations);
            }
        }
        void addOrUpdateLineStation(BO.LineStation station)
        {
            try
            {
                addLineStation(station);
            }
            catch (BO.StationException)
            {
                updateLineStation(station);
            }
        }

        #endregion

        #region Users

        /// <summary>
        /// Func that converts user of BO to user of DO
        /// </summary>
        /// <param name="user">user of BO</param>
        /// <returns>user of DO</returns>
        User convertToUserDO(BO.User user)
        {
            return new User()
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
        BO.User convertToUserBO(User user)
        {
            return new BO.User()
            {
                Username = user.Username,
                Password = user.Password,
                IsManager = user.IsManager
            };
        }
        public void addUser(BO.User user)
        {
            try
            {
                dal.addUser(convertToUserDO(user));
            }
            catch (UserException ex)
            {
                throw new BO.UserException(ex.Message, ex);
            }
        }
        public void removeUser(BO.User user)
        {
            try
            {
                dal.removeUser(convertToUserDO(user));
            }
            catch (UserException ex)
            {
                throw new BO.UserException(ex.Message, ex);
            }
        }
        public void updateUser(BO.User user)
        {
            try
            {
                dal.updateUser(convertToUserDO(user));
            }
            catch (UserException ex)
            {
                throw new BO.UserException(ex.Message, ex);
            }
        }
        public BO.User getUser(string username)
        {
            try
            {
                return convertToUserBO(dal.getUser(username));
            }
            catch (UserException ex)
            {
                throw new BO.UserException(ex.Message, ex);
            }
        }
        public IEnumerable<BO.User> GetUsers()
        {
            try
            {
                return from user in dal.GetUsers()
                       select convertToUserBO(user);
            }
            catch (UserException ex)
            {
                throw new BO.UserException(ex.Message, ex);
            }
        }
        public IEnumerable<BO.User> GetUsers(Predicate<BO.User> condition)
        {
            try
            {
                return from item in GetUsers()
                       where condition(item)
                       select item;
            }
            catch (BO.UserException ex)
            {
                throw new BO.UserException(ex.Message);
            }
        }

        #endregion

        #region Buses

        /// <summary>
        /// Func that converts bus of BO to bus of DO
        /// </summary>
        /// <param name="bus">bus of BO</param>
        /// <returns>bus of DO</returns>
        Bus convertToBusDO(BO.Bus bus)
        {
            return new Bus()
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
        BO.Bus convertToBusBO(Bus bus)
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
            catch (BusException ex)
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
            catch (BusException ex)
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
            catch (BusException ex)
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
            catch (BusException ex)
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
            catch (BusException ex)
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
        /// impossible to change a bus if it is driving
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public bool canChangeBus(BO.Bus bus)
        {
            //if (GetDrivingBuses(item => item.LicensePlate == bus.LicensePlate) == null)
                return true;
            //return false;
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
        Line convertToLineDO(BO.Line lineB)
        {
            return new Line()
            {
                ThisSerial = lineB.ThisSerial,
                NumberLine = lineB.NumberLine,
                Region = (Regions)lineB.Region
            };
        }
        /// <summary>
        /// Func that converts line of DO to line of BO
        /// </summary>
        /// <param name="bus">line of DO</param>
        /// <returns>line of BO</returns>
        BO.Line convertToLineBO(Line lineD)
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
        /// Take from the line data about following stations and enter this data.
        /// </summary>
        /// <param name="line">Using in the path of line.</param>
        void convertLineToFollowingStationDO(BO.Line line)
        {
            for (int i = 0; i < line.Path.Count() - 1; i++)
                addOrUpdateTwoFollowingStations(new TwoFollowingStations()
                {
                    FirstStationID = line.Path.ElementAt(i).ID,
                    SecondStationID = line.Path.ElementAt(i + 1).ID,
                    LengthBetweenStations = line.Path.ElementAt(i + 1).LengthFromPreviousStations,
                    TimeBetweenStations = line.Path.ElementAt(i + 1).TimeFromPreviousStations
                });
        }
        /// <summary>
        /// Take from the path of line data about line stations.
        /// </summary>
        /// <param name="line">using path</param>
        void convertLineToLineStationsDO(BO.Line line)
        {
            foreach (LineStation lineStation in dal.GetLineStations(item => item.NumberLine == line.ThisSerial).ToList())
                dal.removeLineStation(lineStation);
            foreach (BO.LineStation lineStation in line.Path)
                addOrUpdateLineStation(lineStation);
        }
        /// <summary>
        /// Add line from the data.
        /// </summary>
        /// <param name="line">line for add</param>
        public void addLine(BO.Line line)
        {
            try
            {
                if (line.Path.Count() < 2)
                    throw new BO.LineException("A line path should contain at least two stations.");
                int thisSerial = dal.addLine(convertToLineDO(line));
                foreach (BO.LineStation lineStation in line.Path)
                    lineStation.NumberLine = thisSerial;
                convertLineToFollowingStationDO(line);
                convertLineToLineStationsDO(line);
            }
            catch (BO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
            catch (LineException ex)
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
                foreach (LineStation lineStation in dal.GetLineStations(item => item.NumberLine == line.ThisSerial).ToList())
                    dal.removeLineStation(lineStation);
            }
            catch (LineException ex)
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
                convertLineToFollowingStationDO(line);
                foreach (BO.LineStation lineStation in line.Path)
                    lineStation.NumberLine = line.ThisSerial;
                convertLineToLineStationsDO(line);
            }
            catch (LineException ex)
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
            catch (LineException ex)
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
                return (from line in dal.GetLines()
                        select convertToLineBO(line)).OrderBy(item => item.ThisSerial);
            }
            catch (LineException ex)
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
        /// impossible to change a line if it is driving
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool canChangeLine(BO.Line line)
        {
            //if (GetDrivingLines(item => item.NumberLine == line.ThisSerial).Count() == 0)
                return true;
            //return false;
        }
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
        Station convertToStationDO(BO.Station stationB)
        {
            return new Station()
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
        BO.Station convertToStationBO(Station stationD)
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
                {
                    try
                    {
                        TwoFollowingStations followingStations = dal.getTwoFollowingStations(station.ID, otherStation.ID);
                        followingStations.LengthBetweenStations += distanceFromPreviousLocation;
                        followingStations.TimeBetweenStations = calculateTime(followingStations.LengthBetweenStations);
                        dal.updateTwoFollowingStations(followingStations);
                    }
                    catch (StationException) { }
                }
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
            catch (StationException ex)
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
            catch (StationException ex)
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
                if(distanceFromPreviousLocation != 0)
                    stationToFollowingStation(station, distanceFromPreviousLocation);
            }
            catch (StationException ex)
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
            catch (StationException ex)
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
            catch (StationException ex)
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
            //IEnumerable<BO.DrivingLine> drivingLinesAtStation = from drivingLine in GetDrivingLines()
            //                                                    from lineStation in GetLineStations(l => l.ID == station.ID)
            //                                                    where drivingLine.NumberLine == lineStation.NumberLine
            //                                                    select drivingLine;
            //if (drivingLinesAtStation.Count() == 0)
                return true;
            //return false;
        }
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
        LineStation convertToLineStationDO(BO.LineStation lineStationB)
        {
            return new LineStation()
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
        BO.LineStation convertToLineStationBO(LineStation lineStationD)
        {
            BO.LineStation lineStationB = new BO.LineStation()
            {
                NumberLine = lineStationD.NumberLine,
                ID = lineStationD.ID,
                PathIndex = lineStationD.PathIndex
            };
            LineStation lineStationP = dal.GetLineStations(item => item.NumberLine == lineStationD.NumberLine && item.PathIndex == lineStationD.PathIndex - 1).FirstOrDefault();
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
            LineStation lineStationN = dal.GetLineStations(item => item.NumberLine == lineStationD.NumberLine && item.PathIndex == lineStationD.PathIndex + 1).FirstOrDefault();
            if (lineStationN != null)
                lineStationB.NextStationID = lineStationN.ID;
            else // if it is the last line station
                lineStationB.NextStationID = -1;
            return lineStationB;
        }
        public IEnumerable<BO.LineStation> convertToLineStationsList(IEnumerable<BO.Station> path)
        {
            List<BO.LineStation> lineStations = new List<BO.LineStation>();
            TwoFollowingStations followingStations;
            lineStations.Add(new BO.LineStation()
            {
                ID = path.ElementAt(0).ID,
                PathIndex = 1,
                NextStationID = path.ElementAt(1).ID,
                PreviousStationID = -1,
                LengthFromPreviousStations = -1,
                TimeFromPreviousStations = -1
            });
            for (int i = 1; i < path.Count() - 1; i++)
            {
                followingStations = dal.getTwoFollowingStations(path.ElementAt(i).ID, path.ElementAt(i - 1).ID);
                lineStations.Add(new BO.LineStation()
                {
                    ID = path.ElementAt(i).ID,
                    PathIndex = i + 1,
                    NextStationID = path.ElementAt(i + 1).ID,
                    PreviousStationID = path.ElementAt(i - 1).ID,
                    LengthFromPreviousStations = followingStations.LengthBetweenStations,
                    TimeFromPreviousStations = followingStations.TimeBetweenStations
                });
            }
            followingStations = dal.getTwoFollowingStations(path.ElementAt(path.Count() - 1).ID, path.ElementAt(path.Count() - 2).ID);
            lineStations.Add(new BO.LineStation()
            {
                
                ID = path.ElementAt(path.Count() - 1).ID,
                PathIndex = path.Count(),
                NextStationID = -1,
                PreviousStationID = path.ElementAt(path.Count() - 2).ID,
                LengthFromPreviousStations = followingStations.LengthBetweenStations,
                TimeFromPreviousStations = followingStations.TimeBetweenStations
            }); ;
            return lineStations;
        }
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
                BO.LineStation station = GetLineStations(item => item.NumberLine == lineStation.NumberLine && item.PathIndex == lineStation.PathIndex).FirstOrDefault();
                if (station == null)
                    throw new BO.StationException("");
                IEnumerable<BO.LineStation> lineStations = GetLineStations(Station => Station.NumberLine == lineStation.NumberLine).OrderBy(item => item.PathIndex);
                for (int i = lineStation.PathIndex - 1; i < lineStations.Count(); i++)
                {
                    lineStations.ElementAt(i).PathIndex++;
                    updateLineStation(lineStations.ElementAt(i));
                }
            }
            catch (BO.StationException) { }
            finally
            {
                try
                {
                    dal.addLineStation(convertToLineStationDO(lineStation));
                }
                catch (StationException ex)
                {
                    throw new BO.StationException(ex.Message, ex);
                }
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
                IEnumerable<BO.LineStation> lineStations = GetLineStations(Station => Station.NumberLine == lineStation.NumberLine).OrderBy(station => station.PathIndex);
                for (int i = lineStation.PathIndex - 1; i < lineStations.Count(); i++)
                {
                    lineStations.ElementAt(i).PathIndex--;
                    updateLineStation(lineStations.ElementAt(i));
                }
            }
            catch (StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
        }
        /// <summary>
        /// Update data of station in line
        /// </summary>
        /// <param name="lineStation">station in line</param>
        public void updateLineStation(BO.LineStation lineStation)
        {
            try
            {
                BO.LineStation station = getLineStation(lineStation.NumberLine, lineStation.ID); // if the line station exists
                try
                {
                    if (lineStation.PathIndex != station.PathIndex) // if the index is updated
                    {
                        IEnumerable<BO.LineStation> lineStations = GetLineStations(Station => Station.NumberLine == lineStation.NumberLine).OrderBy(item => item.PathIndex);
                        if (station.PathIndex < lineStation.PathIndex) // need to increase the stations index that after the old station
                            for (int i = station.PathIndex - 1; i < lineStation.PathIndex - 1; i++)
                            {
                                lineStations.ElementAt(i).PathIndex--;
                                updateLineStation(lineStations.ElementAt(i));
                            }
                        for (int i = lineStation.PathIndex - 1; i < lineStations.Count(); i++)
                        {
                            lineStations.ElementAt(i).PathIndex++;
                            updateLineStation(lineStations.ElementAt(i));
                        }
                    }
                }
                catch (BO.StationException) { }
                finally
                {
                    dal.updateLineStation(convertToLineStationDO(lineStation));
                }
            }
            catch (StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
            catch (BO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        /// <summary>
        /// Return station in line with the keys: numberLine,id.
        /// </summary>
        /// <param name="numberLine">the line that the station in it path.</param>
        /// <param name="id">number of the station</param>
        /// <returns>station in line</returns>
        public BO.LineStation getLineStation(int numberLine, int id)
        {
            LineStation lineStation= dal.getLineStation(numberLine, id);
            if (lineStation == null)
                return null;
            return convertToLineStationBO(lineStation);

        }
        public IEnumerable<BO.LineStation> GetLineStations()
        {
            try
            {
                return from lineStation in dal.GetLineStations()
                       select convertToLineStationBO(lineStation);
            }
            catch (StationException ex)
            {
                throw new BO.StationException(ex.Message, ex);
            }
        }
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
            catch (StationException ex) { throw new BO.StationException(ex.Message, ex); }
        }
        public bool getTwoFollowingStations(int firstID, int secondID)
        {
            if (dal.getTwoFollowingStations(firstID, secondID) == null)
                return false;
            return true;
        }

        #endregion

        #region Trip

        private int duration(int serial)
        {
            BO.Line line = getLine(serial);
            if (line == null)
                return 0;
            int duration = 1; // because time to previous of the first is -1
            foreach (BO.LineStation lineStation in line.Path)
                duration += lineStation.TimeFromPreviousStations;
            return duration;
        }

        private int getPreviousStationIndex(BO.Line line, TimeSpan timeOfTrip, out int previousStationTime)
        {
            previousStationTime = 0;
            int secondsOfTrip = (int)timeOfTrip.TotalSeconds;
            if(duration(line.ThisSerial) > secondsOfTrip)
                return 0; // not found
            int time = 1; // because time to previous of the first is -1
            foreach (BO.LineStation lineStation in line.Path)
            {
                time += lineStation.TimeFromPreviousStations;
                if (time >= secondsOfTrip)
                {
                    previousStationTime = time - secondsOfTrip;
                    return lineStation.PathIndex - 1;
                }
            }
            return 0; // not found
        }

        private BO.DrivingBus getTrip(int serial, DateTime start)
        {
            BO.Line line = getLine(serial);
            int previousStationTime;
            int index = getPreviousStationIndex(line, DateTime.Now - start, out previousStationTime);
            if (index == 0)
                return null;

            int nextStationTime = line.Path.ElementAt(index).TimeFromPreviousStations - previousStationTime;
            return new BO.DrivingBus()
            {
                NumberLine = serial,
                Start = start,
                PreviousStationID = line.Path.ElementAt(index - 1).ID,
                PreviousStationTime = new TimeSpan(previousStationTime / 3600, previousStationTime % 3600 / 60, previousStationTime % 3600 % 60),
                NextStationTime = new TimeSpan(nextStationTime / 3600, nextStationTime % 3600 / 60, nextStationTime % 3600 % 60)
            };
        }

        public IEnumerable<BO.DrivingBus> GetTripsOfLine(int serial)
        {
            List<BO.DrivingBus> trips = new List<BO.DrivingBus>();
            foreach (DrivingLine drivingLine in dal.GetDrivingLines(item => item.NumberLine == serial).ToList())
                for (TimeSpan i = drivingLine.Start; i < drivingLine.End; i.Add(new TimeSpan(0, drivingLine.Frequency, 0)))
                {
                    BO.DrivingBus trip = getTrip(serial, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i.Hours, i.Minutes, i.Seconds));
                    if (trip != null)
                        trips.Append(trip);
                }
            return trips;
        }

        public IEnumerable<BO.DrivingBus> GetTrips()
        {
            List<BO.DrivingBus> trips = new List<BO.DrivingBus>();
            foreach (DrivingLine drivingLine in dal.GetDrivingLines().ToList())
                for (TimeSpan i = drivingLine.Start; i < drivingLine.End; i.Add(new TimeSpan(0, drivingLine.Frequency, 0)))
                {
                    BO.DrivingBus trip = getTrip(drivingLine.NumberLine, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, i.Hours, i.Minutes, i.Seconds));
                    if (trip != null)
                        trips.Append(trip);
                }
            return trips;
        }

        #endregion

        #region DrivingBuses

        ///// <summary>
        ///// Func that converts driving bus of BO to driving bus of DO
        ///// </summary>
        ///// <param name="drivingBus">driving bus of BO</param>
        ///// <returns>driving bus of DO</returns>
        //DrivingBus convertToDrivingBusDO(BO.DrivingBus drivingBus)
        //{
        //    return new DrivingBus()
        //    {
        //        ThisSerial = drivingBus.ThisSerial,
        //        Line = drivingBus.Line,
        //        LicensePlate = drivingBus.LicensePlate,
        //        ActualStart = drivingBus.ActualStart,
        //        Start = drivingBus.Start,
        //        PreviousStationID = drivingBus.PreviousStationID,
        //        PreviousStationTime = drivingBus.PreviousStationTime,
        //        NextStationTime = drivingBus.NextStationTime
        //    };
        //}
        ///// <summary>
        ///// Func that converts driving bus of DO to driving bus of BO
        ///// </summary>
        ///// <param name="drivingBus">driving bus of DO</param>
        ///// <returns>driving bus of BO</returns>
        //BO.DrivingBus convertToDrivingBusBO(DrivingBus drivingBus)
        //{
        //    return new BO.DrivingBus()
        //    {
        //        LicensePlate = drivingBus.LicensePlate,
        //        Line = drivingBus.Line,
        //        Start = drivingBus.Start,
        //        ThisSerial = drivingBus.ThisSerial,
        //        ActualStart = drivingBus.ActualStart,
        //        PreviousStationID = drivingBus.PreviousStationID,
        //        PreviousStationTime = drivingBus.PreviousStationTime,
        //        NextStationTime = drivingBus.NextStationTime
        //    };
        //}
        //public void addDrivingBus(BO.DrivingBus drivingBus)
        //{
        //    try
        //    {
        //        dal.addDrivingBus(convertToDrivingBusDO(drivingBus));
        //    }
        //    catch (BusException ex)
        //    {
        //        throw new BO.BusException(ex.Message, ex);
        //    }
        //}
        //public void removeDrivingBus(BO.DrivingBus drivingBus)
        //{
        //    try
        //    {
        //        dal.removeDrivingBus(convertToDrivingBusDO(drivingBus));
        //    }
        //    catch (BusException ex)
        //    {
        //        throw new BO.BusException(ex.Message, ex);
        //    }
        //}
        //public void updateDrivingBus(BO.DrivingBus drivingBus)
        //{
        //    try
        //    {
        //        dal.updateDrivingBus(convertToDrivingBusDO(drivingBus));
        //    }
        //    catch (BusException ex)
        //    {
        //        throw new BO.BusException(ex.Message, ex);
        //    }
        //}
        //public BO.DrivingBus getDrivingBus(int thisSerial, string licensePlate, int line, DateTime start)
        //{
        //    try
        //    {
        //        return convertToDrivingBusBO(dal.getDrivingBus(thisSerial, licensePlate, line, start));
        //    }
        //    catch (BusException ex)
        //    {
        //        throw new BO.BusException(ex.Message, ex);
        //    }
        //}
        //public IEnumerable<BO.DrivingBus> GetDrivingBuses()
        //{
        //    try
        //    {
        //        return from drivingBus in dal.GetDrivingBuses()
        //               select convertToDrivingBusBO(drivingBus);
        //    }
        //    catch (BusException ex)
        //    {
        //        throw new BO.BusException(ex.Message, ex);
        //    }
        //}
        //public IEnumerable<BO.DrivingBus> GetDrivingBuses(Predicate<BO.DrivingBus> condition)
        //{
        //    try
        //    {
        //        return from item in GetDrivingBuses()
        //               where condition(item)
        //               select item;
        //    }
        //    catch (BO.BusException ex)
        //    {
        //        throw new BO.BusException(ex.Message);
        //    }
        //}

        #endregion

        #region DrivingLines

        ///// <summary>
        ///// Func that converts driving line of BO to driving line of DO
        ///// </summary>
        ///// <param name="drivingLine">driving line of BO</param>
        ///// <returns>driving line of DO</returns>
        //DrivingLine convertToDrivingLineDO(BO.DrivingLine drivingLine)
        //{
        //    return new DrivingLine()
        //    {
        //        NumberLine = drivingLine.NumberLine,
        //        Start = drivingLine.Start,
        //        End = drivingLine.End,
        //        Frequency = drivingLine.Frequency
        //    };
        //}
        ///// <summary>
        ///// Func that converts driving line of DO to driving line of BO
        ///// </summary>
        ///// <param name="drivingLine">driving line of DO</param>
        ///// <returns>driving line of BO</returns>
        //BO.DrivingLine convertToDrivingLineBO(DrivingLine drivingLine)
        //{
        //    return new BO.DrivingLine()
        //    {
        //        NumberLine = drivingLine.NumberLine,
        //        Start = drivingLine.Start,
        //        Frequency = drivingLine.Frequency,
        //        End = drivingLine.End
        //    };
        //}
        //public void addDrivingLine(BO.DrivingLine drivingLine)
        //{
        //    try
        //    {
        //        dal.addDrivingLine(convertToDrivingLineDO(drivingLine));
        //    }
        //    catch (LineException ex)
        //    {
        //        throw new BO.LineException(ex.Message, ex);
        //    }
        //}
        //public void removeDrivingLine(BO.DrivingLine drivingLine)
        //{
        //    try
        //    {
        //        dal.removeDrivingLine(convertToDrivingLineDO(drivingLine));
        //    }
        //    catch (LineException ex)
        //    {
        //        throw new BO.LineException(ex.Message, ex);
        //    }
        //}
        //public void updateDrivingLine(BO.DrivingLine drivingLine)
        //{
        //    try
        //    {
        //        dal.updateDrivingLine(convertToDrivingLineDO(drivingLine));
        //    }
        //    catch (LineException ex)
        //    {
        //        throw new BO.LineException(ex.Message, ex);
        //    }
        //}
        //public BO.DrivingLine getDrivingLine(int numberLine, DateTime start)
        //{
        //    try
        //    {
        //        return convertToDrivingLineBO(dal.getDrivingLine(numberLine, start));
        //    }
        //    catch (LineException ex)
        //    {
        //        throw new BO.LineException(ex.Message, ex);
        //    }
        //}
        //public IEnumerable<BO.DrivingLine> GetDrivingLines()
        //{
        //    try
        //    {
        //        return from drivingLine in dal.GetDrivingLines()
        //               select convertToDrivingLineBO(drivingLine);
        //    }
        //    catch (LineException ex)
        //    {
        //        throw new BO.LineException(ex.Message, ex);
        //    }
        //}
        //public IEnumerable<BO.DrivingLine> GetDrivingLines(Predicate<BO.DrivingLine> condition)
        //{
        //    try
        //    {
        //        return from item in GetDrivingLines()
        //               where condition(item)
        //               select item;
        //    }
        //    catch (BO.LineException ex)
        //    {
        //        throw new BO.LineException(ex.Message);
        //    }
        //}

        #endregion

        #region ManagingCode

        public string getManagingCode()
        {
            return dal.getManagingCode();
        }
        public void updateManagingCode(string code)
        {
            dal.updateManagingCode(code);
        }

        #endregion
    }
}