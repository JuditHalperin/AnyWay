using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temp
{
    class DrivingBus
    {
        #region
        static private int countID = 1;
        private int drivingBusID; public int DrivingBusID
        {
            get { return drivingBusID; }
            set { drivingBusID = value; }
        }

        private string id; public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private int drivingBusLine; public int DrivingBusLine
        {
            get { return drivingBusLine; }
            set { drivingBusLine = value; }
        }

        private DateTime exitTime; public DateTime ExitTime
        {
            get { return exitTime; }
            set { exitTime = value; }
        }

        private DateTime realExitTime; public DateTime RealExitTime
        {
            get { return realExitTime; }
            set { realExitTime = value; }
        }

        private int lastIDStation; public int LastIDStation
        {
            get { return lastIDStation; }
            set { lastIDStation = value; }
        }

        private DateTime timeInlastStation; public DateTime TimeInlastStation
        {
            get { return timeInlastStation; }
            set { timeInlastStation = value; }
        }

        private DateTime timeInNextStation; public DateTime TimeInNextStation
        {
            get { return timeInNextStation; }
            set { timeInNextStation = value; }
        }
        #endregion
    }
}
