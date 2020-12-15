using System;
using System.Collections.Generic;
using System.Text;
using DO;

namespace DALAPI
{
    public interface IDal
    {
        string SayHello();
        void SetHello(string message);
        // TODO comment
        //CRUD
        bool addBus(BusDAO bus);
        bool update(BusDAO bus);
        void delete(BusDAO bus);
        BusDAO read(int license);
        List<BusDAO> getBusses();
        //TODO for you
        bool addBusInTravel(BusInTravelDAO bus);
        List<BusInTravelDAO> getBusesTravel();
    }
}
