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
            DS.DataSource.buses.Add(bus);
        }
        void removeBus(Bus bus)
        {
            try { DS.DataSource.buses.Remove(bus); }
            catch
            {
                throw new BusException("The bus is not exsits");
            }            
        }
        void updateBus(Bus bus)
        {
            Bus bust = (Bus)(from busTemp in DS.DataSource.buses//Find the bus for update.
                      where busTemp.LicensePlate==bus.LicensePlate
                      select busTemp);
            DS.DataSource.buses.Remove(bust);//Remove the old bus
            DS.DataSource.buses.Add(bus);//Add the update bus
        }
        Bus getBus(string licensePlate)
        {
            Bus bus = from busTemp in DS.DataSource.buses
                                     where busTemp.LicensePlate==licensePlate
                                     select busTemp;
            return bus;
        }
        IEnumerable<Bus> GetBuses()
        {
            return DS.DataSource.buses;
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

        void addLine(Line line);
        void removeLine(Line line);
        void updateLine(Line line);
        Bus getLine(int serial);
        IEnumerable<Line> GetLines();
        IEnumerable<Line> GetLines(Predicate<object> condition);

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
