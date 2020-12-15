using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public interface IBL
    {
        bool insertBus(Bus bus);
        bool updateBus(Bus bus);
        List<Bus> getAllBusses();
        int refuel(Bus bus);
    }
}
