using System;
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
            set { id = value; }
        }

        private double latitude;
        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        private double longitude;
        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public BusStation(string id)
        {

        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
