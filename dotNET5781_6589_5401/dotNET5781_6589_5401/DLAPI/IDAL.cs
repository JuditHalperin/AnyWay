using DO;
using System;
using System.Collections.Generic;

namespace DLAPI
{
    public interface IDAL
    {
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

        int addLine(Line line);
        void removeLine(Line line);
        void updateLine(Line line);
        Line getLine(int serial);
        IEnumerable<Line> GetLines();
        IEnumerable<Line> GetLines(Predicate<Line> condition);
        int countLines();

        #endregion

        #region Stations

        void addStation(Station station);
        void removeStation(Station station);
        void updateStation(Station station);
        Station getStation(int id);
        IEnumerable<Station> GetStations();
        IEnumerable<Station> GetStations(Predicate<Station> condition);
        int countStations();

        #endregion

        #region LineStations

        void addLineStation(LineStation lineStation);
        void removeLineStation(LineStation lineStation);
        void updateLineStation(LineStation lineStation);
        LineStation getLineStation(int numberLine, int id);
        IEnumerable<LineStation> GetLineStations();
        IEnumerable<LineStation> GetLineStations(Predicate<LineStation> condition);

        #endregion

        #region FollowingStations

        void addTwoFollowingStations(TwoFollowingStations twoFollowingStations);
        void removeTwoFollowingStations(TwoFollowingStations twoFollowingStations);
        void updateTwoFollowingStations(TwoFollowingStations twoFollowingStations);
        TwoFollowingStations getTwoFollowingStations(int firstStationID, int secondStationID);
        IEnumerable<TwoFollowingStations> GetFollowingStations();
        IEnumerable<TwoFollowingStations> GetFollowingStations(Predicate<TwoFollowingStations> condition);

        #endregion

        //#region DrivingBuses

        //void addDrivingBus(DrivingBus drivingBus);
        //void removeDrivingBus(DrivingBus drivingBus);
        //void updateDrivingBus(DrivingBus drivingBus);
        //DrivingBus getDrivingBus(int thisSerial, string licensePlate, int line, DateTime start);
        //IEnumerable<DrivingBus> GetDrivingBuses();
        //IEnumerable<DrivingBus> GetDrivingBuses(Predicate<DrivingBus> condition);

        //#endregion

        #region DrivingLines

        void addDrivingLine(DrivingLine drivingLine);
        void removeDrivingLine(DrivingLine drivingLine);
        void updateDrivingLine(DrivingLine drivingLine);
        DrivingLine getDrivingLine(int numberLine, TimeSpan start);
        IEnumerable<DrivingLine> GetDrivingLines();
        IEnumerable<DrivingLine> GetDrivingLines(Predicate<DrivingLine> condition);

        #endregion
        
        #region ManagingCode
        string getManagingCode();
        void updateManagingCode(string code);
        #endregion
    }
}