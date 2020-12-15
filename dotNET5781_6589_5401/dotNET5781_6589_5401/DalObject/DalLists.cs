using DALAPI;
using DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalLists : IDal
    {

        private String myString = "kuku strike again";
        public string SayHello()
        {
            return myString;
        }

        public void SetHello(string message)
        {
            myString = message;
        }

        public bool addBus(BusDAO bus)
        {
            if (DataSource.Buses.Exists(mishehu => mishehu.License == bus.License))
            {
                return false;
            }

            BusDAO realBus = new BusDAO
            {
                License = bus.License,
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms
            };

            DataSource.Buses.Add(realBus);
            return true;
        }

        public bool update(BusDAO bus)
        {
            if (!DataSource.Buses.Exists(mishehu => mishehu.License == bus.License))
            {
                return false;
            }

            /**
            Bus realBus = DataSource.Buses.First(mishehu => mishehu.License == bus.License);
            realBus.StartOfWork = bus.StartOfWork;
            realBus.TotalKms = bus.TotalKms;
            **/
            //delete
            DataSource.Buses.RemoveAll(b => b.License == bus.License);
            //insert
            DataSource.Buses.Add(new BusDAO
            {
                License = bus.License,
                StartOfWork = bus.StartOfWork,
                TotalKms = bus.TotalKms
            });

            return true;
        }

        public List<BusDAO> getBusses()
        {
            List<BusDAO> result = new List<BusDAO>();
            foreach (var bus in DS.DataSource.Buses)
            {
                result.Add(new BusDAO
                {
                    License = bus.License,
                    StartOfWork = bus.StartOfWork,
                    TotalKms = bus.TotalKms
                });
            }
            return result;
        }

        public BusDAO read(int license)
        {
            BusDAO result = default(BusDAO);
            result = DS.DataSource.Buses.FirstOrDefault(item => item.License == license);
            if (result != null)
            {
                return new BusDAO     //clone (!) clown
                {
                    License = result.License,
                    StartOfWork = result.StartOfWork,
                    TotalKms = result.TotalKms
                };
            }
            return null;
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
