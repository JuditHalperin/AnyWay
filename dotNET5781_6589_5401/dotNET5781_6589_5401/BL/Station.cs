using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class Station : INotifyPropertyChanged
    {
        //static private Random rand = new Random(DateTime.Now.Millisecond);
        //static private int code = 1000;

        public event PropertyChangedEventHandler PropertyChanged;

        #region
        private string id; public string ID
        {
            get { return id; }
            private set { id = value; }
        }
        private string name; public string Name
        {
            get { return name; }
            private set
            {
                name = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        private double latitude; public double Latitude // קו רוחב 
        {
            get { return latitude; }
            private set
            {
                latitude = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Latitude"));
            }
        }
        private double longitude; public double Longitude // קו אורך 
        {
            get { return longitude; }
            private set
            {
                longitude = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Longitude"));
            }
        }
        #endregion

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="id">id of station</param>
        /// <param name="name">name of station</param>
        /// <param name="latitude">latitude of station</param>
        /// <param name="longitude">longitude of station</param>
        public Station(string id, string name, double latitude, double longitude)
        {
            ID = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;

            //ID = Convert.ToString(code++);
            //Latitude = rand.Next(31000000, 33300000) / 1000000.0; // real number bwtween [31, 33.3] with 6 digits after the point
            //Longitude = rand.Next(34300000, 35500000) / 1000000.0; // real number bwtween [34.3, 35.5] with 6 digits after the point
        }

        /// <summary>
        /// calculates the distance between two stations
        /// </summary>
        /// <param name="second">second station</param>
        /// <returns>meters between two stations</returns>
        public double distanceBetweenStations(Station second)
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
            return $"{ID} {Name} ({Latitude}°N, {Longitude}°E)";
        }
    }
}