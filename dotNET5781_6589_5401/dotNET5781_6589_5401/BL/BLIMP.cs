using System;
using System.Collections.Generic;
using System.Linq;
using System.Device.Location;
using BLAPI;
using DLAPI;
using DO;

namespace BL
{
    public class BLIMP : IBL
    {
        readonly IDAL dal = DalFactory.GetDal();

        //#region Singelton

        //static readonly BLIMP instance = new BLIMP();
        //public static BLIMP Instance => instance;
        //static BLIMP() { }
        //BLIMP() { }

        //#endregion

        #region Help functions

        int calculateDistance(BO.Station first, BO.Station second)
        {
            GeoCoordinate positionThisStation = new GeoCoordinate(first.Latitude, first.Longitude);
            GeoCoordinate positionSecondStation = new GeoCoordinate(second.Latitude, second.Longitude);
            return (int)positionThisStation.GetDistanceTo(positionSecondStation);
        }
        int calculateTime(int distance)
        {
            return (int)(distance * 0.001);//according to valocity of 60 Km per hour.
        }
        void addOrUpdateTwoFollowingStations(DO.TwoFollowingStations followingStations)
        {
            try
            {
                dal.addTwoFollowingStations(followingStations);
            }
            catch (DO.StationException)
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
        public void addUser(BO.User user)
        {
            try
            {
                dal.addUser(convertToUserDO(user));
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException(ex.Message);
            }
        }
        public void removeUser(BO.User user)
        {
            try
            {
                dal.removeUser(convertToUserDO(user));
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException(ex.Message);
            }
        }
        public void updateUser(BO.User user)
        {
            try
            {
                dal.updateUser(convertToUserDO(user));
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException(ex.Message);
            }
        }
        public BO.User getUser(string username)
        {
            try
            {
                return convertToUserBO(dal.getUser(username));
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException(ex.Message);
            }
        }
        public IEnumerable<BO.User> GetUsers()
        {
            try
            {
                IEnumerable<DO.User> usersD = dal.GetUsers();
                IEnumerable<BO.User> usersB = from user in usersD
                                              select convertToUserBO(user);
                return usersB;
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException(ex.Message);
            }
        }
        public IEnumerable<BO.User> GetUsers(Predicate<BO.User> condition)
        {
            try
            {
                IEnumerable<BO.User> users = from item in GetUsers()
                                             where condition(item)
                                             select item;
                if (users.Count() == 0)
                    throw new BO.UserException("No users exist.");
                return users;
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
                KmsSinceService = bus.KmsSinceService,
            };
            busB.Status = setState(busB);
            return busB;
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
                throw new BO.BusException(ex.Message);
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
                throw new BO.BusException(ex.Message);
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
                throw new BO.BusException(ex.Message);
            }
        }
        /// <summary>
        /// fuel bus=KmsSinceFuel = 0
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
            catch(BO.BusException)
            {
                throw new BO.BusException("The bus is not exists");
            }
        }
        /// <summary>
        /// Service in bus and fuel when needed.
        /// </summary>
        /// <param name="bus">bus for service</param>
        public void serviceBus(BO.Bus bus)
        {
            try
            {
                bus = getBus(bus.LicensePlate);
                bus.KmsSinceService = 0;
                if (bus.KmsSinceFuel > 1100)//Need refueling soon
                    bus.KmsSinceFuel = 0;
                bus.LastService = DateTime.Now.Date;
                dal.updateBus(convertToBusDO(bus));
            }
            catch (BO.BusException)
            {
                throw new BO.BusException("The bus is not exists");
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
                throw new BO.BusException(ex.Message);
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
                IEnumerable<DO.Bus> busesD = dal.GetBuses();
                IEnumerable<BO.Bus> busesB = from bus in busesD
                                             select convertToBusBO(bus);
                return busesB;
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
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
                IEnumerable<BO.Bus> buses = from item in GetBuses()
                                            where condition(item)
                                            select item;
                if (buses.Count() == 0)
                    throw new BO.BusException("No buses exist.");
                return buses;
            }
            catch (BO.BusException ex)
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
            IEnumerable<BO.LineStation> lineStationsB = GetLineStations(lineStation => lineStation.NumberLine == lineD.ThisSerial).OrderBy(station => station.PathIndex);
            return new BO.Line()
            {
                NumberLine = lineD.NumberLine,
                Region = (BO.Regions)lineD.Region,
                Path = lineStationsB
            };
        }
        /// <summary>
        /// Take from the line data about following stations and enter this data.
        /// </summary>
        /// <param name="line">Using in the path of line.</param>
        void convertLineToFollowingStationDO(BO.Line line)
        {
            TwoFollowingStations followingStations = new TwoFollowingStations();
            for (int i = 0; i < line.Path.Count() - 1; i++)
            {
                followingStations.FirstStationID = line.Path.ElementAt(i).ID;
                followingStations.SecondStationID = line.Path.ElementAt(i + 1).ID;
                followingStations.LengthBetweenStations = line.Path.ElementAt(i + 1).LengthFromPreviousStations;
                followingStations.TimeBetweenStations = line.Path.ElementAt(i + 1).TimeFromPreviousStations;
                addOrUpdateTwoFollowingStations(followingStations);
            }
        }
        /// <summary>
        /// Take from the path of line data about line stations.
        /// </summary>
        /// <param name="line">using path</param>
        void convertLineToLineStationsDO(BO.Line line)
        {
            foreach (BO.LineStation station in line.Path)
            {
                addOrUpdateLineStation(station);
            }
        }
        /// <summary>
        /// Add line from the data.
        /// </summary>
        /// <param name="line">line for add</param>
        public void addLine(BO.Line line)
        {
            try
            {
                dal.addLine(convertToLineDO(line));
                convertLineToFollowingStationDO(line);
                convertLineToLineStationsDO(line);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
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
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
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
                convertLineToLineStationsDO(line);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }
        /// <summary>
        /// Return line according it key= the serial.
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
                throw new BO.LineException(ex.Message);
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
                IEnumerable<DO.Line> linesD = dal.GetLines();
                IEnumerable<BO.Line> linesB = from line in linesD
                                              select convertToLineBO(line);
                return linesB;
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
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
                IEnumerable<BO.Line> lines = from item in GetLines()
                                             where condition(item)
                                             select item;
                if (lines.Count() == 0)
                    throw new BO.LineException("No lines exist.");
                return lines;
            }
            catch (BO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
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
                IEnumerable<BO.LineStation> lineStationsB = GetLineStations(lineStation => lineStation.ID == stationD.ID).OrderBy(station => station.ID);
                return new BO.Station()
                {
                    ID = stationD.ID,
                    Name = stationD.Name,
                    Latitude = stationD.Latitude,
                    Longitude = stationD.Longitude,
                    LinesAtStation=lineStationsB
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
        void stationToFollowingStationAndLineStation(BO.Station station)
        {
            IEnumerable<BO.Station> stations = GetStations();
            foreach (BO.Station station1 in stations)
            {
                TwoFollowingStations followingStations = new TwoFollowingStations()
                {
                    FirstStationID = station.ID,
                    SecondStationID = station1.ID,
                    LengthBetweenStations = calculateDistance(station, station1),
                };
                followingStations.TimeBetweenStations = calculateTime(followingStations.LengthBetweenStations);
                addOrUpdateTwoFollowingStations(followingStations);
            }

        }
        /// <summary>
        /// impossible to change a station if there are driving lines that stop there
        /// </summary>
        /// <param name="station"></param>
        /// <returns></returns>
        public bool canChangeStation(BO.Station station)
        {
            IEnumerable<BO.DrivingLine> drivingLinesAtStation = from drivingLine in GetDrivingLines()
                                                                from lineStation in GetLineStations(l => l.ID == station.ID)
                                                                where drivingLine.NumberLine == lineStation.NumberLine
                                                                select drivingLine;
            if (drivingLinesAtStation.Count() == 0)
                return true;
            return false;
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
                stationToFollowingStationAndLineStation(station);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
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
                foreach (BO.LineStation lineStation in GetLineStations(item => item.ID != station.ID))
                    removeLineStation(lineStation);
            }
            catch (StationException ex)
            {
                throw new BO.StationException(ex.Message);
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
        public void updateStation(BO.Station station)
        {
            try
            {
                dal.updateStation(convertToStationDO(station));
                stationToFollowingStationAndLineStation(station);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
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
                throw new BO.StationException(ex.Message);
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
                IEnumerable<DO.Station> stationsD = dal.GetStations();
                IEnumerable<BO.Station> stationsB = from station in stationsD
                                                    select convertToStationBO(station);
                return stationsB;
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
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
                IEnumerable<BO.Station> stations = from item in GetStations()
                                                   select item;
                if (stations.Count() == 0)
                    throw new BO.StationException("No stations exist.");
                return stations;
            }
            catch (BO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
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
        BO.LineStation convertToLineStationBO(LineStation lineStationD)
        {
            BO.LineStation lineStationB = new BO.LineStation()
            {
                NumberLine = lineStationD.NumberLine,
                ID = lineStationD.ID,
                PathIndex = lineStationD.PathIndex
            };
            try
            {
                IEnumerable<BO.LineStation> lineStationsB = GetLineStations(lineStation => lineStation.NumberLine == lineStationD.NumberLine).OrderBy(station => station.PathIndex);
                TwoFollowingStations followingStations = new TwoFollowingStations();
                lineStationB.NextStationID = lineStationsB.ElementAt(lineStationB.PathIndex + 1).ID;
                lineStationB.PreviousStationID = lineStationsB.ElementAt(lineStationB.PathIndex - 1).ID;
                followingStations = dal.getTwoFollowingStations(lineStationB.ID, lineStationB.NextStationID);
                lineStationB.LengthFromPreviousStations = followingStations.LengthBetweenStations;
                lineStationB.TimeFromPreviousStations = followingStations.TimeBetweenStations;

            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
            return lineStationB;
        }
        /// <summary>
        /// Add station in line
        /// </summary>
        /// <param name="lineStation">station in line</param>
        public void addLineStation(BO.LineStation lineStation)
        {
            try
            {
                try
                {
                    BO.LineStation station=GetLineStations(item => (item.NumberLine == lineStation.NumberLine && item.PathIndex == lineStation.PathIndex)).First();
                    IEnumerable<BO.LineStation> lineStations = GetLineStations(Station => Station.NumberLine == lineStation.NumberLine).OrderBy(item => item.PathIndex);
                    for (int i = lineStation.PathIndex-1; i < lineStations.Count(); i++)
                    {
                        lineStations.ElementAt(i).PathIndex++;
                        updateLineStation(lineStations.ElementAt(i));
                    }
                }
                catch(BO.BusException) { }
                finally
                {
                    dal.addLineStation(convertToLineStationDO(lineStation));
                }
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
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
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
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
                BO.LineStation station = getLineStation(lineStation.NumberLine,lineStation.ID);//if the station is exists.
                try
                {
                    if(lineStation.PathIndex!=station.PathIndex)//if the update in the index of path
                    {
                        IEnumerable<BO.LineStation> lineStations = GetLineStations(Station => Station.NumberLine == lineStation.NumberLine).OrderBy(item => item.PathIndex);
                        if(station.PathIndex< lineStation.PathIndex)//need to back the station that after the old station
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
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
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
            try
            {
                return convertToLineStationBO(dal.getLineStation(numberLine, id));
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        public IEnumerable<BO.LineStation> GetLineStations()
        {
            try
            {
                IEnumerable<DO.LineStation> lineStationD = dal.GetLineStations();
                IEnumerable<BO.LineStation> linetationB = from lineStation in lineStationD
                                                          select convertToLineStationBO(lineStation);
                return linetationB;
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
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

        #region DrivingBuses

        /// <summary>
        /// Func that converts driving bus of BO to driving bus of DO
        /// </summary>
        /// <param name="drivingBus">driving bus of BO</param>
        /// <returns>driving bus of DO</returns>
        DO.DrivingBus convertToDrivingBusDO(BO.DrivingBus drivingBus)
        {
            return new DO.DrivingBus()
            {
                ThisSerial = drivingBus.ThisSerial,
                Line = drivingBus.Line,
                LicensePlate = drivingBus.LicensePlate,
                ActualStart = drivingBus.ActualStart,
                Start = drivingBus.Start,
                PreviousStationID = drivingBus.PreviousStationID,
                PreviousStationTime = drivingBus.PreviousStationTime,
                NextStationTime = drivingBus.NextStationTime
            };
        }
        /// <summary>
        /// Func that converts driving bus of DO to driving bus of BO
        /// </summary>
        /// <param name="drivingBus">driving bus of DO</param>
        /// <returns>driving bus of BO</returns>
        BO.DrivingBus convertToDrivingBusBO(DO.DrivingBus drivingBus)
        {
            return new BO.DrivingBus()
            {
                LicensePlate = drivingBus.LicensePlate,
                Line = drivingBus.Line,
                Start = drivingBus.Start,
                ThisSerial = drivingBus.ThisSerial,
                ActualStart = drivingBus.ActualStart,
                PreviousStationID = drivingBus.PreviousStationID,
                PreviousStationTime = drivingBus.PreviousStationTime,
                NextStationTime = drivingBus.NextStationTime
            };
        }
        public void addDrivingBus(BO.DrivingBus drivingBus)
        {
            try
            {
                dal.addDrivingBus(convertToDrivingBusDO(drivingBus));
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        public void removeDrivingBus(BO.DrivingBus drivingBus)
        {
            try
            {
                dal.removeDrivingBus(convertToDrivingBusDO(drivingBus));
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        public void updateDrivingBus(BO.DrivingBus drivingBus)
        {
            try
            {
                dal.updateDrivingBus(convertToDrivingBusDO(drivingBus));
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        public BO.DrivingBus getDrivingBus(int thisSerial, string licensePlate, int line, DateTime start)
        {
            try
            {
                return convertToDrivingBusBO(dal.getDrivingBus(thisSerial, licensePlate, line, start));
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        public IEnumerable<BO.DrivingBus> GetDrivingBuses()
        {
            try
            {
                IEnumerable<DO.DrivingBus> drivingBusesD = dal.GetDrivingBuses();
                IEnumerable<BO.DrivingBus> drivingBusesB = from drivingBus in drivingBusesD
                                                           select convertToDrivingBusBO(drivingBus);
                return drivingBusesB;
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        public IEnumerable<BO.DrivingBus> GetDrivingBuses(Predicate<BO.DrivingBus> condition)
        {
            try
            {
                IEnumerable<BO.DrivingBus> drivingBuses = from item in GetDrivingBuses()
                                                          where condition(item)
                                                          select item;
                if (drivingBuses.Count() == 0)
                    throw new BO.BusException("No driving buses exist.");
                return drivingBuses;
            }
            catch (BO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
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
        public void addDrivingLine(BO.DrivingLine drivingLine)
        {
            try
            {
                dal.addDrivingLine(convertToDrivingLineDO(drivingLine));
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }
        public void removeDrivingLine(BO.DrivingLine drivingLine)
        {
            try
            {
                dal.removeDrivingLine(convertToDrivingLineDO(drivingLine));
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }
        public void updateDrivingLine(BO.DrivingLine drivingLine)
        {
            try
            {
                dal.updateDrivingLine(convertToDrivingLineDO(drivingLine));
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }
        public BO.DrivingLine getDrivingLine(int numberLine, DateTime start)
        {
            try
            {
                return convertToDrivingLineBO(dal.getDrivingLine(numberLine, start));
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }
        public IEnumerable<BO.DrivingLine> GetDrivingLines()
        {
            try
            {
                IEnumerable<DO.DrivingLine> drivingLinesD = dal.GetDrivingLines();
                IEnumerable<BO.DrivingLine> drivingLinesB = from drivingLine in drivingLinesD
                                                            select convertToDrivingLineBO(drivingLine);
                return drivingLinesB;
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }
        public IEnumerable<BO.DrivingLine> GetDrivingLines(Predicate<BO.DrivingLine> condition)
        {
            try
            {
                IEnumerable<BO.DrivingLine> drivingLines = from item in GetDrivingLines()
                                                           where condition(item)
                                                           select item;
                if (drivingLines.Count() == 0)
                    throw new BO.LineException("No driving lines exist.");
                return drivingLines;
            }
            catch (BO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }

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