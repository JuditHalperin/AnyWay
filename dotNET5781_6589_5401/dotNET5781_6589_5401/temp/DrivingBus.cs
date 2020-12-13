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

        private string licensePlate; public string LicensePlate
        {
            get { return licensePlate; }
            set { licensePlate = value; }
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
        /// <summary>
        /// constractor
        /// </summary>
        /// <param name="licensePlate">License Number (Entity Key - Part 1)</param>
        /// <param name="drivingBusLine">Line ID in Execution (Entity Key - Part 2)</param>
        /// <param name="exitTime">Departure time to the formal line (Entity key - part 3)</param>
        /// <param name="realExitTime">Actual departure time</param>
        /// <param name="lastIDStation">Last stop number on the line that the bus passed</param>
        /// <param name="timeInlastStation">Transit time at the last stop mentioned above</param>
        /// <param name="timeInNextStation">Time to get to the next station</param>
        public DrivingBus(string licensePlate, int drivingBusLine, DateTime exitTime, DateTime realExitTime, int lastIDStation, DateTime timeInlastStation, DateTime timeInNextStation)
        {
            DrivingBusID = countID++;
            LicensePlate = licensePlate;
            DrivingBusLine = drivingBusLine;
            ExitTime = exitTime;
            RealExitTime = realExitTime;
            LastIDStation = lastIDStation;
            TimeInlastStation = timeInlastStation;
            TimeInNextStation = timeInNextStation;
        }
    }
}
