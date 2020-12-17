using System;
using BLAPI;
using DLAPI;
//using DL;
using BO;
using DO;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace BL
{
    public class BLIMP : IBL
    {
        readonly IDAL dal = DalFactory.GetDal();

        #region Users
        DO.User convertToUserDO(BO.User user)
        {
            DO.User userD = new DO.User();
            userD.Username = user.Username;
            userD.Password = user.Password;
            userD.IsManager = user.IsManager;
            return userD;
        }
        BO.User convertToUserBO(DO.User user)
        {
            return new BO.User(user.Username, user.Password, user.IsManager);
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
                IEnumerable<BO.User> users = GetUsers();
                users = from item in users
                        where condition(item)
                        select item;
                if (users.Count() == 0)
                    throw new BO.UserException("No users exist.");
                return users;
            }
            catch(BO.UserException ex)
            {
                throw new BO.UserException(ex.Message);
            }
        }

        #endregion

        #region Buses
        /// <summary>
        /// Func that convert bus of BO for bus of DO
        /// </summary>
        /// <param name="bus">bus of BO</param>
        /// <returns>bus of DO</returns>
        DO.Bus convertToBusDO(BO.Bus bus)
        {
            DO.Bus busD = new DO.Bus();
            busD.LicensePlate = bus.LicensePlate;
            busD.StartOfWork = bus.StartOfWork;
            busD.Status = (DO.State)bus.Status;
            busD.TotalKms = bus.TotalKms;
            busD.KmsSinceFuel = bus.KmsSinceFuel;
            busD.KmsSinceService = bus.KmsSinceService;
            busD.LastService = bus.LastService;
            return busD;
        }
        /// <summary>
        /// Func that convert bus of DO for bus of BO
        /// </summary>
        /// <param name="bus">bus of DO</param>
        /// <returns>bus of BO</returns>
        BO.Bus convertToBusBO(DO.Bus bus)
        {
            BO.Bus busB = new BO.Bus(bus.StartOfWork, bus.LastService, bus.LicensePlate, bus.TotalKms, bus.KmsSinceFuel, bus.KmsSinceService);
            return busB;
        }
        public void addBus(BO.Bus bus)
        {
            DO.Bus busD = convertToBusDO(bus);
            try
            {
                dal.addBus(busD);
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        public void removeBus(BO.Bus bus)
        {
            DO.Bus busD = convertToBusDO(bus);
            try
            {
                dal.removeBus(busD);
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        public void updateBus(BO.Bus bus)
        {
            DO.Bus busD = convertToBusDO(bus);
            try
            {
                dal.updateBus(busD);
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        public BO.Bus getBus(string licensePlate)
        {
            try
            {
                DO.Bus bus = dal.getBus(licensePlate);
                return convertToBusBO(bus);
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
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
        public IEnumerable<BO.Bus> GetBuses(Predicate<BO.Bus> condition)
        {
            try
            {
                IEnumerable<BO.Bus> buses = GetBuses();
                buses = from item in buses
                        where condition(item)
                        select item;
                if (buses.Count() == 0)
                    throw new BO.BusException("No buses exist.");
                return buses;
            }
            catch(BO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }


        #endregion

        #region Lines
        /// <summary>
        /// Func that convert Line of BO for Line of DO
        /// </summary>
        /// <param name="bus">Line of BO</param>
        /// <returns>Line of DO</returns>
        DO.Line convertToLineDO(BO.Line lineB)
        {
            DO.Line lineD = new DO.Line();
            lineD.ThisSerial = lineB.ThisSerial;
            lineD.NumberLine = lineB.NumberLine;
            lineD.FirstStation = lineB.FirstStation;
            lineD.LastStation = lineB.LastStation;
            lineD.Region = (DO.Regions)lineB.Region;
            return lineD;
        }
        /// <summary>
        /// Func that convert Line of DO for Line of BO
        /// </summary>
        /// <param name="bus">Line of DO</param>
        /// <returns>Line of BO</returns>
        BO.Line convertToLineBO(DO.Line lineD)
        {
            IEnumerable<DO.LineStation> stations = (IEnumerable<DO.LineStation>)dal.GetLineStations(Station => Station.NumberLine == lineD.ThisSerial);
            stations = stations.OrderBy(station => station.PathIndex);
            IEnumerable<BO.LineStation> stationsB = from item in stations
                                                    select convertToLineStationBO(item);
            TwoFollowingStations followingStations = new TwoFollowingStations();
            try
            {
                for (int i = 0; i < stationsB.Count() - 1; i++)
                {
                    followingStations = dal.getTwoFollowingStations(stationsB.ElementAt(i).ID, stationsB.ElementAt(i + 1).ID);
                    stationsB.ElementAt(i + 1).LengthFromPreviousStations = followingStations.LengthBetweenStations;
                    stationsB.ElementAt(i + 1).TimeFromPreviousStations = followingStations.TimeBetweenStations;
                }
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }

            return new BO.Line(lineD.NumberLine, (BO.Regions)lineD.Region, (ObservableCollection<BO.LineStation>)stationsB);
        }
        void convertLineToFollowingStationDO(BO.Line line)
        {
            
            for (int i = 0; i < line.Path.Count() - 1; i++)
            {

                TwoFollowingStations followingStations = new TwoFollowingStations();
                followingStations.FirstStationID = line.Path.ElementAt(i).ID;
                followingStations.SecondStationID = line.Path.ElementAt(i + 1).ID;
                followingStations.LengthBetweenStations = line.Path.ElementAt(i + 1).LengthFromPreviousStations;
                followingStations.TimeBetweenStations = line.Path.ElementAt(i + 1).TimeFromPreviousStations;
                addOrUpdateTwoFollowingStations(followingStations);
            }

        }
        void addOrUpdateTwoFollowingStations(DO.TwoFollowingStations followingStations)
        {
            try
            {
                dal.addTwoFollowingStations(followingStations);
            }
            catch(DO.StationException)
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
        void convertLineToLineStationsDO(BO.Line line)
        {
            foreach(BO.LineStation station in line.Path)
            {
                addOrUpdateLineStation(station);
            }
        }
        public void addLine(BO.Line line)
        {
            DO.Line lineD = convertToLineDO(line);
            convertLineToFollowingStationDO(line);
            convertLineToLineStationsDO(line);
            try
            {
                dal.addLine(lineD);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }
        public void removeLine(BO.Line line)
        {
            DO.Line lineD = convertToLineDO(line);
            try
            {
                dal.removeLine(lineD);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }

        }
        public void updateLine(BO.Line line)
        {
            DO.Line lineD = convertToLineDO(line);
            convertLineToFollowingStationDO(line);
            convertLineToLineStationsDO(line);
            try
            {
                dal.updateLine(lineD);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
            

        }
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
        public IEnumerable<BO.Line> GetLines(Predicate<BO.Line> condition)
        {
            try
            {
                IEnumerable<BO.Line> lines = GetLines();
                lines = from item in lines
                        where condition(item)
                        select item;
                if (lines.Count() == 0)
                    throw new BO.LineException("No lines exist.");
                return lines;
            }
            catch(BO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }


        #endregion

        #region Stations
        DO.Station convertToStationDO(BO.Station stationB)
        {
            DO.Station stationD = new DO.Station();
            stationD.ID = stationB.ID;
            stationD.Name = stationB.Name;
            stationD.Latitude = stationB.Latitude;
            stationD.Longitude = stationB.Longitude;
            return stationD;
        }
        BO.Station convertToStationBO(DO.Station stationD)
        {
            return new BO.Station(stationD.ID, stationD.Name, stationD.Latitude, stationD.Longitude);
        }
        public void addStation(BO.Station station)
        {
            try
            {
                dal.addStation(convertToStationDO(station));
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        public void removeStation(BO.Station station)
        {
            try
            {
                dal.removeStation(convertToStationDO(station));
                IEnumerable<BO.LineStation> lineStations = GetLineStations(item => item.ID != station.ID);
                foreach(BO.LineStation lineStation in lineStations)
                {
                    removeLineStation(lineStation);
                }
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        public void updateStation(BO.Station station)
        {
            try
            {
                dal.updateStation(convertToStationDO(station));
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
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
        public IEnumerable<BO.Station> GetStations(Predicate<BO.Station> condition)
        {
            try
            {
                IEnumerable<BO.Station> stations = GetStations();
                stations = from item in stations
                           select item;
                if (stations.Count() == 0)
                    throw new BO.StationException("No stations exist.");
                return stations;
            }
            catch(BO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }

        #endregion

        #region LineStations
        DO.LineStation convertToLineStationDO(BO.LineStation lineStationB)
        {
            DO.LineStation lineStationD = new DO.LineStation();
            lineStationD.ID = lineStationB.ID;
            lineStationD.NumberLine = lineStationB.NumberLine;
            lineStationD.PathIndex = lineStationB.PathIndex;
            return lineStationD;
        }
        BO.LineStation convertToLineStationBO(DO.LineStation lineStationD)
        {
            return new BO.LineStation(lineStationD.NumberLine, lineStationD.ID, lineStationD.PathIndex);
        }
        public void addLineStation(BO.LineStation lineStation)
        {
            DO.LineStation lineStationD = convertToLineStationDO(lineStation);
            try
            {
                dal.addLineStation(lineStationD);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        public void removeLineStation(BO.LineStation lineStation)
        {
            DO.LineStation lineStationD = convertToLineStationDO(lineStation);
            try
            {
                dal.removeLineStation(lineStationD);
                IEnumerable<BO.LineStation> stations = GetLineStations(Station => Station.NumberLine == lineStation.NumberLine);
                stations = stations.OrderBy(station => station.PathIndex);
                for (int i = lineStation.PathIndex - 1; i < stations.Count(); i++)
                {
                    stations.ElementAt(i).PathIndex--;
                    updateLineStation(stations.ElementAt(i));
                }
                if(lineStation.PathIndex == stations.Count())
                {
                    DO.Line line = dal.getLine(lineStation.NumberLine);
                    line.LastStation = stations.ElementAt(stations.Count() - 1).ID;
                }
                if (lineStation.PathIndex == 1)
                {
                    DO.Line line = dal.getLine(lineStation.NumberLine);
                    line.FirstStation = stations.First().ID;
                }
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        public void updateLineStation(BO.LineStation lineStation)
        {
            DO.LineStation lineStationD = convertToLineStationDO(lineStation);
            try
            {
                dal.updateLineStation(lineStationD);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        public BO.LineStation getLineStation(int numberLine, int id)
        {
            try
            {
                return convertToLineStationBO(getLineStation(numberLine, id));
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
                IEnumerable<BO.LineStation> lineStations = GetLineStations();
                lineStations = from item in lineStations
                               where condition(item)
                               select item;
                if (lineStations.Count() == 0)
                    throw new BO.StationException("No line stations exist.");
                return lineStations;
            }
            catch(BO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }

        #endregion

        #region DrivingBuses
        DO.DrivingBus convertToDrivingBusDO(BO.DrivingBus drivingBus)
        {
            DO.DrivingBus drivingBusD = new DO.DrivingBus();
            drivingBusD.ThisSerial = drivingBus.ThisSerial;
            drivingBusD.Line = drivingBus.Line;
            drivingBusD.LicensePlate = drivingBus.LicensePlate;
            drivingBusD.ActualStart = drivingBus.ActualStart;
            drivingBusD.Start = drivingBus.Start;
            drivingBusD.PreviousStationID = drivingBus.PreviousStationID;
            drivingBusD.PreviousStationTime = drivingBus.PreviousStationTime;
            drivingBusD.NextStationTime = drivingBus.NextStationTime;
            return drivingBusD;
        }
        BO.DrivingBus convertToDrivingBusBO(DO.DrivingBus drivingBus)
        {
            BO.DrivingBus drivingBusB = new BO.DrivingBus(drivingBus.LicensePlate, drivingBus.Line, drivingBus.Start);
            //drivingBusB.ThisSerial = drivingBusD.ThisSerial;
            //drivingBusB.ActualStart = drivingBus.ActualStart;
            //drivingBusB.Start = drivingBus.Start;
            //drivingBusB.PreviousStationID = drivingBus.PreviousStationID;
            //drivingBusB.PreviousStationTime = drivingBus.PreviousStationTime;
            //drivingBusB.NextStationTime = drivingBus.NextStationTime;
            return drivingBusB;
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
                IEnumerable<BO.DrivingBus> drivingBuses = GetDrivingBuses();
                drivingBuses = from item in drivingBuses
                               where condition(item)
                               select item;
                if (drivingBuses.Count() == 0)
                    throw new BO.BusException("No driving buses exist.");
                return drivingBuses;
            }
            catch(BO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }


        #endregion

        #region DrivingLines
        DO.DrivingLine convertToDrivingLineDO(BO.DrivingLine drivingLine)
        {
            DO.DrivingLine drivingLineD = new DO.DrivingLine();
            drivingLineD.NumberLine = drivingLine.NumberLine;
            drivingLineD.Start = drivingLine.Start;
            drivingLineD.End = drivingLine.End;
            drivingLineD.Frequency = drivingLine.Frequency;
            return drivingLineD;
        }
        BO.DrivingLine convertToDrivingLineBO(DO.DrivingLine drivingLine)
        {
            return new BO.DrivingLine(drivingLine.NumberLine, drivingLine.Start, drivingLine.Frequency, drivingLine.End);
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
                IEnumerable<BO.DrivingLine> drivingLines = GetDrivingLines();
                drivingLines = from item in drivingLines
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
    }
}