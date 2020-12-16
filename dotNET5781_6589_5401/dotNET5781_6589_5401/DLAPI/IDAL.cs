﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI
{
    public interface IDAL
    {
        #region users

        void addUser(User user);
        void removeUser(User user);
        void updateUser(User user);
        Bus getUser(string username);
        IEnumerable<Station> GetUsers();
        IEnumerable<Station> GetUsers(Predicate<User> condition);

        #endregion

        #region buses

        void addBus(Bus bus);
        void removeBus(Bus bus);
        void updateBus(Bus bus);
        Bus getBus(string licensePlate);
        IEnumerable<Bus> GetBuses();
        IEnumerable<Bus> GetBuses(Predicate<Bus> condition);

        #endregion

        #region lines

        void addLine(Line line);
        void removeLine(Line line);
        void updateLine(Line line);
        Bus getLine(int serial);
        IEnumerable<Line> GetLines();
        IEnumerable<Line> GetLines(Predicate<Line> condition);

        #endregion

        #region stations

        void addStation(Station station);
        void removeStation(Station station);
        void updateStation(Station station);
        Bus getStation(int id);
        IEnumerable<Station> GetStations();
        IEnumerable<Station> GetStations(Predicate<Station> condition);

        #endregion
       
        #region lineStations

        void addLineStation(LineStation lineStation);
        void removeLineStation(LineStation lineStation);
        void updateLineStation(LineStation lineStation);
        Bus getLineStation(int numberLine, int id);
        IEnumerable<Station> GetLineStations();
        IEnumerable<Station> GetLineStations(Predicate<LineStation> condition);

        #endregion

        #region followingStations

        void addTwoFollowingStations(TwoFollowingStations twoFollowingStations);
        void removeTwoFollowingStations(TwoFollowingStations twoFollowingStations);
        void updateTwoFollowingStations(TwoFollowingStations twoFollowingStations);
        Bus getTwoFollowingStations(int firstStationID, int secondStationID);
        IEnumerable<Station> GetFollowingStations();
        IEnumerable<Station> GetFollowingStations(Predicate<TwoFollowingStations> condition);

        #endregion

        #region drivingBuses

        void addDrivingBus(DrivingBus drivingBus);
        void removeDrivingBus(DrivingBus drivingBus);
        void updateDrivingBus(DrivingBus drivingBus);
        Bus getDrivingBus(int thisSerial, string licensePlate, int line, DateTime start);
        IEnumerable<Station> GetDrivingBuses();
        IEnumerable<Station> GetDrivingBuses(Predicate<DrivingBus> condition);

        #endregion

        #region drivingLines

        void addDrivingLine(DrivingLine drivingLine);
        void removeDrivingLine(DrivingLine drivingLine);
        void updateDrivingLine(DrivingLine drivingLine);
        Bus getDrivingLine(int thisSerial, string licensePlate, int line, DateTime start);
        IEnumerable<Station> GetDrivingLines();
        IEnumerable<Station> GetDrivingLines(Predicate<DrivingLine> condition);

        #endregion
    }
}