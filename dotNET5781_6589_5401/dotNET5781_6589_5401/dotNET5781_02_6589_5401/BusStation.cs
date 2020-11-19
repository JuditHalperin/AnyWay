/*
Judit Halperin - 324216589
Asnat Kahane - 211825401

Exercise 2
8/11/20
This program implements the BusStation class, the BusLine class and a collection of buses.
*/

using System;
using System.Device.Location;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    public class BusStation
    {
        static private Random rand = new Random(DateTime.Now.Millisecond);

        static private int code = 1000;

        private string id;
        public string ID
        {
            get { return id; }
            private set { id = value; }
        }

        private double latitude;
        public double Latitude // קו רוחב 
        {
            get { return latitude; }
            set { latitude = value; }
        }

        private double longitude;
        public double Longitude // קו אורך 
        {
            get { return longitude; }
            set { longitude = value; }
        }

        /// <summary>
        /// constructor
        /// (random values to latitude and longitude)
        /// </summary>
        public BusStation()
        {
            ID = Convert.ToString(code++);
            Latitude = rand.Next(31000000, 33300000) / 1000000.0; // real number bwtween [31, 33.3] with 6 digits after the point
            Longitude = rand.Next(34300000, 35500000) / 1000000.0; // real number bwtween [34.3, 35.5] with 6 digits after the point
        }
     
        /// <summary>
        /// calculates the distance between two stations
        /// </summary>
        /// <param name="second">second station</param>
        /// <returns>meters between two stations</returns>
        public double distanceBetweenStations(BusStation second)
        {            
            GeoCoordinate positionThisStation = new GeoCoordinate(Latitude, Longitude);
            GeoCoordinate positionSecondStation = new GeoCoordinate(second.Latitude, second.Longitude);
            return positionThisStation.GetDistanceTo(positionSecondStation);
        }

        /// <summary>
        /// describe the station's attributes
        /// </summary>
        /// <returns>string of code and location on the globe</returns>
        public override string ToString()
        {
            return $"{ID} ({Latitude}°N, {Longitude}°E)";
        }
    }
}