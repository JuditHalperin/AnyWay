using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DrivingBus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static int serial = 1;

        #region
        private int thisSerial; public int ThisSerial
        {
            get { return thisSerial; }
            private set { thisSerial = value; }
        }
        private string licensePlate; public string LicensePlate
        {
            get { return licensePlate; }
            private set { licensePlate = value; }
        }
        private int line;  public int Line
        {
            get { return line; }
            private set { line = value; }
        }
        private DateTime start; public DateTime Start
        {
            get { return start; }
            private set { start = value; }
        }
        private DateTime actualStart; public DateTime ActualStart
        {
            get { return actualStart; }
            private set
            {
                actualStart = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ActualStart"));
            }
        }
        private int previousStationID; public int PreviousStationID
        {
            get { return previousStationID; }
            private set
            {
                previousStationID = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PreviousStationID"));
            }
        }
        private DateTime previousStationTime; public DateTime PreviousStationTime
        {
            get { return previousStationTime; }
            private set
            {
                previousStationTime = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PreviousStationTime"));
            }
        }
        private DateTime nextStationTime; public DateTime NextStationTime
        {
            get { return nextStationTime; }
            private set
            {
                nextStationTime = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("NextStationTime"));
            }
        }

        #endregion

        public DrivingBus(string licensePlate, int line, DateTime start)
        {
            ThisSerial = serial++;
            LicensePlate = licensePlate;
            Line = line;
            Start = start;
            ActualStart = DateTime.Now;
            PreviousStationID = 0;
            PreviousStationTime = DateTime.Now;
            NextStationTime = DateTime.Now;
        }
    }
}
