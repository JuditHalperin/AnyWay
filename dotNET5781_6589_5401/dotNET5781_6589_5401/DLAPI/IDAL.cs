using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI

{
    public interface IDAL
    {
        #region buses

        void addBus(Bus bus);
        void removeBus(Bus bus);
        void updateBus(Bus bus);
        Bus getBus(string licensePlate);
        IEnumerable<Bus> GetBuses();
        IEnumerable<Bus> GetBuses(Predicate<object> condition);

        #endregion

        #region lines

        void addLine(Line line);
        void removeLine(Line line);
        void updateLine(Line line);
        Bus getLine(int serial);
        IEnumerable<Line> GetLines();
        IEnumerable<Line> GetLines(Predicate<object> condition);

        #endregion

        //double GetTemparture(int day);
        //WindDirection GetWindDirection(int day);
    }
}