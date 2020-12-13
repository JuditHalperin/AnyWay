using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temp
{
    class DrivingUser
    {
        static private int countID = 1;

        #region
        private int drivingUserID; public int DrivingUserID
        {
            get { return drivingUserID; }
            set { drivingUserID = value; }
        }
        private string username; public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private int drivingBusLine; public int DrivingBusLine
        {
            get { return drivingBusLine; }
            set { drivingBusLine = value; }
        }
        private int beginStationID; public int BeginStationID
        {
            get { return beginStationID; }
            set { beginStationID = value; }
        }
        private DateTime gettingOnBusTime; public DateTime GettingOnBusTime
        {
            get { return gettingOnBusTime; }
            set { gettingOnBusTime = value; }
        }
        private int endStationID; public int EndStationID
        {
            get { return endStationID; }
            set { endStationID = value; }
        }
        private DateTime gettingOffBusTime; public DateTime GettingOffBusTime
        {
            get { return gettingOffBusTime; }
            set { gettingOffBusTime = value; }
        }

        #endregion

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="name">username</param>
        /// <param name="line">driving bus line</param>
        /// <param name="beginID">begin station ID</param>
        /// <param name="beginTime">time of getting on the bus</param>
        /// <param name="endID">end station ID</param>
        /// <param name="endTime">time of getting off the bus</param>
        public DrivingUser(string name, int line, int beginID, DateTime beginTime, int endID, DateTime endTime)
        {
            DrivingUserID = countID++;
            Username = name;
            DrivingBusLine = line;
            BeginStationID = beginID;
            GettingOnBusTime = beginTime;
            EndStationID = endID;
            GettingOffBusTime = endTime;
        }
    }
}
