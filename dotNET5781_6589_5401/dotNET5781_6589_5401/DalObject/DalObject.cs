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
            if (!DS.DataSource.Buses.Exists(item => item.LicensePlate == bus.LicensePlate))
                throw new BusException("The bus already exsits.");
            DS.DataSource.Buses.Add(bus);
        }
        void removeBus(Bus bus)
        {
            if(!DS.DataSource.Buses.Remove(bus))
                throw new BusException("The bus is not exsits");           
        }
        void updateBus(Bus bus)
        {
            Bus busT;
            try
            {
                busT = (from busTemp in DS.DataSource.Buses//Find the bus for update.
                                 where busTemp.LicensePlate == bus.LicensePlate
                                 select busTemp).First();
            }
            catch
            {
                throw new BusException("There is not exsits bus with same license of the bus for update.");
            }
            DS.DataSource.Buses.Remove(busT);//Remove the old bus
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
            Line lineT;
            try
            {
                lineT = (from lineTemp in DS.DataSource.Lines//Find the bus for update.
                             where lineTemp.ThisSerial == line.ThisSerial
                             select lineTemp).First();
            }
            catch
            {
                throw new LineException("There is not exsits line with same serial of the line for update.");
            }
            DS.DataSource.Lines.Remove(lineT);//Remove the old bus
            DS.DataSource.Lines.Add(line);//Add the update bus
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
            return DS.DataSource.Lines.Clone();
        }
        IEnumerable<Line> GetLines(Predicate<Line> condition)
        {
            IEnumerable<Line> lines = from line in DS.DataSource.Lines
                                     where condition(line)
                                     select line;
            return lines;
        }

        #endregion
       
    }
}
