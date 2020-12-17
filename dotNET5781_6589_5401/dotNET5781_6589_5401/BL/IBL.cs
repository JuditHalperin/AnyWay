using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BLAPI
{
    public interface IBL
    {
        //Weather GetWeather(int day);
        #region Users

        void addUser(User user);
        void removeUser(User user);
        void updateUser(User user);
        User getUser(string username);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(Predicate<User> condition);

        #endregion

        #region Buses
        void addBus(Bus bus);
        void removeBus(Bus bus);
        void updateBus(Bus bus);
        Bus getBus(string licensePlate);
        IEnumerable<Bus> GetBuses();
        IEnumerable<Bus> GetBuses(Predicate<Bus> condition);

        #endregion

        #region Lines

        void addLine(Line line);
        void removeLine(Line line);
        void updateLine(Line line);
        Line getLine(int serial);
        IEnumerable<Line> GetLines();
        //IEnumerable<Line> GetLines(Predicate<Line> condition);

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
        IEnumerable<LineStation> GetLineStations();
        //IEnumerable<Station> GetLineStations(Predicate<LineStation> condition);

        #endregion

        #region DrivingBuses

        void addDrivingBus(DrivingBus drivingBus);
        void removeDrivingBus(DrivingBus drivingBus);
        DrivingBus getDrivingBus(int thisSerial, string licensePlate, int line, DateTime start);
        IEnumerable<DrivingBus> GetDrivingBuses();
        //IEnumerable<DrivingBus> GetDrivingBuses(Predicate<DrivingBus> condition);

        #endregion

        #region DrivingLines

        void addDrivingLine(DrivingLine drivingLine);
        void removeDrivingLine(DrivingLine drivingLine);
        DrivingLine getDrivingLine(int numberLine, DateTime start);
        IEnumerable<DrivingLine> GetDrivingLines();
        //IEnumerable<DrivingLine> GetDrivingLines(Predicate<DrivingLine> condition);

        #endregion
    }
}
