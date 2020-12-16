using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;

namespace DL
{
    public class DalObject : IDAL
    {
        #region singelton

        static readonly DalObject instance = new DalObject();
        static DalObject() { }
        DalObject() { }
        public static DalObject Instance => instance;

        #endregion

        #region buses

        void addBus(Bus bus)
        {
            DS.DataSource.Buses.Add(bus);
        }
        void removeBus(Bus bus)
        {
            if(!DS.DataSource.Buses.Remove(bus))
                throw new BusException("The bus is not exsits");           
        }
        void updateBus(Bus bus)
        {
            Bus bust = (Bus)(from busTemp in DS.DataSource.Buses//Find the bus for update.
                      where busTemp.LicensePlate==bus.LicensePlate
                      select busTemp);
            DS.DataSource.Buses.Remove(bust);//Remove the old bus
            DS.DataSource.Buses.Add(bus);//Add the update bus
        }
        Bus getBus(string licensePlate)
        {
            Bus bus = (Bus)(from busTemp in DS.DataSource.Buses
                                     where busTemp.LicensePlate==licensePlate
                                     select busTemp);
            return bus;
        }
        IEnumerable<Bus> GetBuses()
        {
            return DS.DataSource.Buses;
        }
        IEnumerable<Bus> GetBuses(Predicate<Bus> condition)
        {
            IEnumerable<Bus> buses = from bus in DS.DataSource.buses
                                     where condition(bus)
                                     select bus;
            return buses;
        }

        #endregion

        #region lines

        void addLine(Line line)
        {
            DS.DataSource.Lines.Add(line);
        }
        void removeLine(Line line)
        {

        }
        void updateLine(Line line)
        {

        }
        Bus getLine(int serial)
        {

        }
        IEnumerable<Line> GetLines()
        {

        }
        IEnumerable<Line> GetLines(Predicate<object> condition)
        {

        }

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

        void addTwoFollowingStations(TwoFollowingStations lineStation);
        void removeTwoFollowingStations(TwoFollowingStations lineStation);
        void updateTwoFollowingStations(TwoFollowingStations lineStation);
        Bus getTwoFollowingStations(int numberLine, int id);
        IEnumerable<Station> GetFollowingStations();
        IEnumerable<Station> GetFollowingStations(Predicate<TwoFollowingStations> condition);

        #endregion


        //static Random rnd = new Random(DateTime.Now.Millisecond);
        //double temperature;

        //public double GetTemparture(int day)
        //{
        //    temperature = rnd.NextDouble() * 50 - 10;
        //    temperature += rnd.NextDouble() * 10 - 5;//122
        //    return temperature;
        //}

        //public WindDirection GetWindDirection(int day)
        //{
        //    WindDirection direction = DataSource.directions.Find(d => true);
        //    var directions = (WindDirections[])Enum.GetValues(typeof(WindDirections));
        //    direction.direction = directions[rnd.Next(0, directions.Length)];

        //    return direction.Clone();
        //}
    }
}
