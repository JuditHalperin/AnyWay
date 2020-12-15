using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALAPI;
using DO;

namespace DAL
{
    public sealed class DalXML : IDal
    {
        private static IDal mydal = new DalXML();

        private DalXML() { }
        static DalXML() { }

        public static IDal Instance
        {
            get
            {
                return mydal;
            }
        }

        public bool addBus(BusDAO bus)
        {
            throw new NotImplementedException();
        }

        public List<BusDAO> getBusses()
        {
            throw new NotImplementedException();
        }

        public string SayHello()
        {
            return "nobody is here";
        }

        public void SetHello(string message)
        {
            throw new NotImplementedException();
        }

        public bool update(BusDAO bus)
        {
            throw new NotImplementedException();
        }

        public BusDAO read(int license)
        {
            throw new NotImplementedException();
        }

        public bool addBusInTravel(BusInTravelDAO bus)
        {
            throw new NotImplementedException();
        }

        public List<BusInTravelDAO> getBusesTravel()
        {
            throw new NotImplementedException();
        }

        public void delete(BusDAO bus)
        {
            throw new NotImplementedException();
        }
    }
}