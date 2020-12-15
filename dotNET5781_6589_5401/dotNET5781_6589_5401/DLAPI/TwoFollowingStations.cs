using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    class TwoFollowingStations
    {
        #region
        private int firstStationID; public int FirstStationID
        {
            get { return firstStationID; }
            set { firstStationID = value; }
        }
        private int secondStationID; public int SecondStationID
        {
            get { return secondStationID; }
            set { secondStationID = value; }
        }
        private float lengthBetweenStations; public float LengthBetweenStations // meters
        {
            get { return lengthBetweenStations; }
            set { lengthBetweenStations = value; }
        }
        private int timeBetweenStations; public int TimeBetweenStations // minutes
        {
            get { return timeBetweenStations; }
            set { timeBetweenStations = value; }
        }

        #endregion
    }
}
