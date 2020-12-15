using DALAPI;
using DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public sealed class DalObject : IDal
    {
        #region singleton implementaion
        private readonly static IDal mydal = new DalObject();

        private DalObject()
        {
            try
            {
                DS.DataSource.initBuses();

            }
            catch (BusException be)
            {
                //TODO
            }
        }

        static DalObject() { }

        public static IDal Instance { get => mydal; }

        #endregion singleton

        #region IDal implementation
        public string SayHello()
        {
            return DataSource.Hello;
        }

        public void SetHello(string message)
        {
            DataSource.Hello = message;
        }

        public bool addBus(BusDAO bus)
        {
            if (DataSource.Buses.Exists(mishehu => mishehu.License == bus.License))
            {
                throw new BusException("license exists allready");
                //return false;
            }

            //BusDAO realBus = new BusDAO //clone
            //{
            //    License = bus.License,
            //    StartOfWork = bus.StartOfWork,
            //    TotalKms = bus.TotalKms
            //};

            //DataSource.Buses.Add(realBus);
            DataSource.Buses.Add(bus.Clone());
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
            //DataSource.Buses.Add(new BusDAO
            //{
            //    License = bus.License,
            //    StartOfWork = bus.StartOfWork,
            //    TotalKms = bus.TotalKms
            //});
            DataSource.Buses.Add(bus.Clone());
            return true;

        }

        public List<BusDAO> getBusses()
        {
            List<BusDAO> result = new List<BusDAO>();
            foreach (var bus in DS.DataSource.Buses)
            {
                result.Add(bus.Clone());
            }
            return result;
        }

        public BusDAO read(int license)
        {
            BusDAO result = default(BusDAO);
            result = DS.DataSource.Buses.FirstOrDefault(item => item.License == license);
            if (result != null)
            {
                //return new BusDAO     //clone (!) clown
                //{
                //    License = result.License,
                //    StartOfWork = result.StartOfWork,
                //    TotalKms = result.TotalKms
                //};
                return result.Clone();
            }
            return null;
        }

        public bool addBusInTravel(BusInTravelDAO bus)
        {
            if (DataSource.BusesTravel.Exists(mishehu =>
                mishehu.License == bus.License
                && mishehu.Line == bus.Line
                && mishehu.Start == bus.Start))
            {
                // throw new InvalidOperationException("license exists allready")
                return false;
            }

            DataSource.BusesTravel.Add(bus.Clone());
            return true;
        }

        public List<BusInTravelDAO> getBusesTravel()
        {
            List<BusInTravelDAO> travels = new List<BusInTravelDAO>();
            foreach (var busInTravel in DS.DataSource.BusesTravel)
            {
                travels.Add(busInTravel.Clone());
            }
            return travels;
        }

        public void delete(BusDAO bus)
        {
            if (!DS.DataSource.Buses.Exists(item => item.License == bus.License))
            {
                //return false;
                throw new BusException("lo kayam bammarechet");
            }
            //BusDAO todelete = null;
            //foreach (var item in DS.DataSource.Buses)
            //{
            //    if(item.License == bus.License)
            //    {
            //        todelete = item;
            //        break;
            //    }
            //}
            //if(todelete != null)
            //{
            //    DS.DataSource.Buses.Remove(todelete);
            //}

            DS.DataSource.Buses.RemoveAll(item => item.License == bus.License);
        }
        #endregion
    }
}