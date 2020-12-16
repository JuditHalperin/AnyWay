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
            Bus bust;
            try
            {
                bust = (from busTemp in DS.DataSource.Buses//Find the bus for update.
                                 where busTemp.LicensePlate == bus.LicensePlate
                                 select busTemp).First();
            }
            catch
            {
                throw new BusException("There is not exsits bus with same license of the bus for update.");
            }
            DS.DataSource.Buses.Remove(bust);//Remove the old bus
            DS.DataSource.Buses.Add(bus);//Add the update bus
        }
        Bus getBus(string licensePlate)
        {
            Bus bus;
            try
            {
                bus = (from busTemp in DS.DataSource.Buses
                           where busTemp.LicensePlate == licensePlate
                           select busTemp).First();
            }
            catch
            {
                throw new BusException("There is not exsits bus with this license.");
            }
            return bus;
        }
        IEnumerable<Bus> GetBuses()
        {
            return DS.DataSource.Buses;
        }
        IEnumerable<Bus> GetBuses(Predicate<Bus> condition)
        {
            IEnumerable<Bus> buses = from bus in DS.DataSource.Buses
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
            if (!DS.DataSource.Lines.Remove(line))
                throw new LineException("The line is not exsits");
        }
        void updateLine(Line line)
        {
            Line linet;
            try
            {
                linet = (from lineTemp in DS.DataSource.Lines//Find the bus for update.
                             where lineTemp.ThisSerial == line.ThisSerial
                             select lineTemp).First();
            }
            catch
            {
                throw new LineException("There is not exsits line with same serial of the line for update.");
            }
            DS.DataSource.Lines.Remove(linet);//Remove the old bus
            DS.DataSource.Lines.Add(linet);//Add the update bus
        }
        Line getLine(int serial)
        {
            Line line;
            try
            {
                line = (from lineTemp in DS.DataSource.Lines//Find the bus for update.
                         where lineTemp.ThisSerial == serial
                         select lineTemp).First();
            }
            catch
            {
                throw new LineException("There is not exsits line with this serial.");
            }
            return line;
        }
        IEnumerable<Line> GetLines()
        {
            return DS.DataSource.Lines;
        }
        IEnumerable<Line> GetLines(Predicate<Line> condition)
        {
            IEnumerable<Line> lines = from line in DS.DataSource.Lines
                                     where condition(line)
                                     select line;
            return lines;
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
