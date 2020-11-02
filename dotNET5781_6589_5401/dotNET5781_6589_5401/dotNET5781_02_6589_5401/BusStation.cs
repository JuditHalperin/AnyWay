﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    class BusStation
    {
        static private Random rand = new Random(DateTime.Now.Millisecond);

        private string id;
        public string ID
        {
            get { return id; }
            set
            {
                int number = int.Parse(value); // optional exeption
                if (number <= 0 || number > 999999)
                    throw;
                id = value;
            }
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
        /// <param name="id">station ID</param>
        public BusStation(string id)
        {
            ID = id;
            Latitude = rand.Next(31000000, 33300000) / 1000000.0; // real number bwtween [31, 33.3] with 6 digits after the point
            Longitude = rand.Next(34300000, 35500000) / 1000000.0; // real number bwtween [34.3, 35.5] with 6 digits after the point
        }

        /// <summary>
        /// describe the station's attributes
        /// </summary>
        /// <returns>string of code and location on the globe</returns>
        public override string ToString()
        {
            return $"Bus Station Code: {ID}, {Latitude}°N {Longitude}°E";
        }
    }
}