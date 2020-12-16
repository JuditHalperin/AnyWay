using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        #region
        private int id; public int ID
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
        public Station(int id, string name, double latitude, double longitude)
        {
            ID = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
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