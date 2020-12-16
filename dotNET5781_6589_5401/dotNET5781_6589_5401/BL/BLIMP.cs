using System;
using BLAPI;
using DLAPI;
//using DL;
using BO;
using DO;
using System.Collections.Generic;
using System.Linq;

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
            IEnumerable<DO.Bus> busesD = dal.GetBuses();
            IEnumerable<BO.Bus> busesB = from bus in busesD
                                         select convertToBusBO(bus);
            return busesB;
        }
        //IEnumerable<BO.Bus> GetBuses(Predicate<BO.Bus> condition)
        //{

        //    IEnumerable<DO.Bus> busesD = dal.GetBuses();
        //    IEnumerable<BO.Bus> busesB = from bus in busesD
        //                                 select convertToBusBO(bus);
        //    return busesB;
        //}

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
            
        }
        void addLine(Line line);
        void removeLine(Line line);
        void updateLine(Line line);
        Line getLine(int serial);
        IEnumerable<Line> GetLines();
        IEnumerable<Line> GetLines(Predicate<Line> condition);

        #endregion

        #region Stations

        void addStation(Station station);
        void removeStation(Station station);
        void updateStation(Station station);
        Station getStation(int id);
        IEnumerable<Station> GetStations();
        IEnumerable<Station> GetStations(Predicate<Station> condition);

        #endregion

        #region LineStations

        void addLineStation(LineStation lineStation);
        void removeLineStation(LineStation lineStation);
        void updateLineStation(LineStation lineStation);
        LineStation getLineStation(int numberLine, int id);
        IEnumerable<Station> GetLineStations();
        IEnumerable<Station> GetLineStations(Predicate<LineStation> condition);

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