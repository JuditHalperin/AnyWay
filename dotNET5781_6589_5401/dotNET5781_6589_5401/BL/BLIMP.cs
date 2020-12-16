using System;
using BLAPI;
using DLAPI;
//using DL;
using BO;
using DO;
using System.Collections.Generic;

namespace BL
{
    public class BLIMP : IBL
    {
        #region Buses

        void addBus(BO.Bus bus)
        {
            DO.Bus busD = new DO.Bus();
            busD.LicensePlate = bus.LicensePlate;
            busD.StartOfWork = bus.StartOfWork;
            busD.Status = bus.Status;
            busD.TotalKms = bus.TotalKms;
        }
        void removeBus(BO.Bus bus)
        {

        }
        void updateBus(BO.Bus bus)
        {

        }
        BO.Bus getBus(string licensePlate)
        {

        }
        IEnumerable<BO.Bus> GetBuses()
        {

        }
        IEnumerable<BO.Bus> GetBuses(Predicate<BO.Bus> condition)
        {

        }

        #endregion

        #region Lines

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