using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region
        private int numberLine; public int NumberLine
        {
            get { return numberLine; }
            private set { numberLine = value; }
        }
        private int id; public int ID
        {
            get { return id; }
            private set { id = value; }
        }
        private int pathIndex; public int PathIndex
        {
            get { return pathIndex; }
            private set
            {
                pathIndex = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PathIndex"));
            }
        }
        private double lengthFromPreviousStations; public double LengthFromPreviousStations // meters
        {
            get { return lengthFromPreviousStations; }
            set
            {
                lengthFromPreviousStations = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("LengthFromPreviousStations"));
            }
        }
        private int timeFromPreviousStations; public int TimeFromPreviousStations // minutes   
        {
            get { return timeFromPreviousStations; }
            set
            {
                timeFromPreviousStations = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TimeFromPreviousStations"));
            }
        }

        #endregion
    }
}
