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
        void addBus(BO.Bus bus)
        {
            DO.Bus busD = convertToBusDO(bus);
            try
            {
                dal.addBus(busD);
            }
            catch(DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        void removeBus(BO.Bus bus)
        {
            DO.Bus busD= convertToBusDO(bus);
            try
            {
                dal.removeBus(busD);
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        void updateBus(BO.Bus bus)
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
        BO.Bus getBus(string licensePlate)
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
        IEnumerable<BO.Bus> GetBuses()
        {
            try
            {
                IEnumerable<DO.Bus> busesD = dal.GetBuses();
                IEnumerable<BO.Bus> busesB = from bus in busesD
                                             select convertToBusBO(bus);
                return busesB;
            }
            catch(DO.BusException ex)
            {
                throw new BO.BusException(ex.Message);
            }
        }
        //IEnumerable<BO.Bus> GetBuses(Predicate<BO.Bus> condition)
      

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
            IEnumerable<DO.LineStation> stations = (IEnumerable<DO.LineStation>)dal.GetLineStations(Station => Station.NumberLine == lineD.NumberLine);
            stations = stations.OrderBy(station => station.PathIndex);
            IEnumerable<BO.LineStation> stationsB = from item in stations
                                                    select convertToLineStationBO(item);
            TwoFollowingStations followingStations = new TwoFollowingStations();
            try
            {
                for (int i = 0; i < stationsB.Count() - 1; i++)
                {
                    followingStations= dal.getTwoFollowingStations(stationsB.ElementAt(i).ID, stationsB.ElementAt(i + 1).ID);
                    stationsB.ElementAt(i + 1).LengthFromPreviousStations = followingStations.LengthBetweenStations;
                    stationsB.ElementAt(i + 1).TimeFromPreviousStations = followingStations.TimeBetweenStations;
                }
            }
            catch(DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
                       
            return new BO.Line(lineD.NumberLine, (BO.Regions)lineD.Region, (ObservableCollection<BO.LineStation>)stationsB);
        }
        void addLine(BO.Line line)
        {
            DO.Line lineD = convertToLineDO(line);
            try
            {
                dal.addLine(lineD);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }
        void removeLine(BO.Line line)
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
        void updateLine(BO.Line line)
        {
            DO.Line lineD = convertToLineDO(line);
            try
            {
                dal.updateLine(lineD);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException(ex.Message);
            }
        }
        BO.Line getLine(int serial)
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
        IEnumerable<BO.Line> GetLines()
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
        //IEnumerable<BO.Line> GetLines(Predicate<BO.Line> condition)


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
        void addStation(BO.Station station)
        {
            try
            {
                dal.addStation(convertToStationDO(station));
            }
            catch(DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        void removeStation(BO.Station station)
        {
            try
            {
                dal.removeStation(convertToStationDO(station));
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        void updateStation(BO.Station station)
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
        BO.Station getStation(int id)
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
        IEnumerable<BO.Station> GetStations()
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
        //IEnumerable<Station> GetStations(Predicate<Station> condition);

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
        void addLineStation(BO.LineStation lineStation)
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
        void removeLineStation(BO.LineStation lineStation)
        {
            DO.LineStation lineStationD = convertToLineStationDO(lineStation);
            try
            {
                dal.removeLineStation(lineStationD);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException(ex.Message);
            }
        }
        void updateLineStation(BO.LineStation lineStation)
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
        BO.LineStation getLineStation(int numberLine, int id)
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
        IEnumerable<BO.LineStation> GetLineStations()
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
        //IEnumerable<Station> GetLineStations(Predicate<LineStation> condition);

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


        //    static Random rnd = new Random(DateTime.Now.Millisecond);

        //    readonly IDAL dal = DalFactory.GetDal();

        //    public Weather GetWeather(int day)
        //    {
        //        Weather w = new Weather();
        //        double feeling;
        //        WindDirections dir;


        //        feeling = dal.GetTemparture(day);
        //        dir = dal.GetWindDirection(day).direction;

        //        switch (dir)
        //        {
        //            case WindDirections.S:
        //                feeling += 2;
        //                break;
        //            case WindDirections.SSE:
        //                feeling += 1.5;
        //                break;
        //            case WindDirections.SE:
        //                feeling += 1;
        //                break;
        //            case WindDirections.SEE:
        //                feeling += 0.5;
        //                break;
        //            case WindDirections.E:
        //                feeling -= 0.5;
        //                break;
        //            case WindDirections.NEE:
        //                feeling -= 1;
        //                break;
        //            case WindDirections.NE:
        //                feeling -= 1.5;
        //                break;
        //            case WindDirections.NNE:
        //                feeling -= 2;
        //                break;
        //            case WindDirections.N:
        //                feeling -= 3;
        //                break;
        //            case WindDirections.NNW:
        //                feeling -= 2.5;
        //                break;
        //            case WindDirections.NW:
        //                feeling -= 2;
        //                break;
        //            case WindDirections.NWW:
        //                feeling -= 1.5;
        //                break;
        //            case WindDirections.W:
        //                feeling -= 1;
        //                break;
        //            case WindDirections.SWW:
        //                feeling -= 0;
        //                break;
        //            case WindDirections.SW:
        //                break;
        //            case WindDirections.SSW:
        //                feeling += 1;
        //                break;
        //        }
        //        w.Feeling = (int)feeling;
        //        return w;
        //    }
        //}

    }