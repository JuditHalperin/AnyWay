using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus : INotifyPropertyChanged
    {
        static private Random rand = new Random(DateTime.Now.Millisecond);

        public event PropertyChangedEventHandler PropertyChanged;

        #region
        private string licensePlate; public string LicensePlate
        {
            get { return licensePlate; }
            private set { licensePlate = value; }
        }
        private DateTime startOfWork; public DateTime StartOfWork
        {
            get { return startOfWork; }
            private set { startOfWork = value; }
        }
        private DateTime lastService; public DateTime LastService
        {
            get { return lastService; }
            set
            {
                lastService = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("LastService"));
            }
        }
        private int totalKms; public int TotalKms
        {
            get { return totalKms; }
            private set
            {
                totalKms = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalKms"));
            }
        }
        private int kmsSinceFuel; public int KmsSinceFuel
        {
            get { return kmsSinceFuel; }
            set
            {
                kmsSinceFuel = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("KmsSinceFuel"));
            }
        }
        private int kmsSinceService; public int KmsSinceService
        {
            get { return kmsSinceService; }
            set
            {
                kmsSinceService = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("KmsSinceService"));
            }
        }
        private State status; public State Status
        {
            get { return status; }
            set
            {
                status = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Status"));
            }
        }
        private bool canBeFueled; public bool CanBeFueled
        {
            get { return canBeFueled; }
            private set
            {
                canBeFueled = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CanBeFueled"));
            }
        }
        private bool canBeServiced; public bool CanBeServiced
        {
            get { return canBeServiced; }
            private set
            {
                canBeServiced = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CanBeServiced"));
            }
        }
        private string time; public string Time
        {
            get { return time; }
            set
            {
                time = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Time"));
            }
        }

        #endregion

        public override string ToString() => this.ToStringProperty();

        /// <summary>
        /// constructor
        /// change dates and length if they are wrong
        /// </summary>
        /// <param name="start">date of start</param>
        /// <param name="service">date of last service</param>
        /// <param name="licensePlate">license plate number</param>
        /// <param name="totalKms">total kms since start</param>
        /// <param name="kmsSinceFuel">total kms since the last fuel</param>
        /// <param name="kmsSinceService">total kms since the last service</param>
        public Bus(DateTime start, DateTime service, string licensePlate, int totalKms, int kmsSinceFuel = 0, int kmsSinceService = 0)
        {
            if (service > DateTime.Now)
                service = DateTime.Now.Date;
            if (start > service)
                start = service;
            
            StartOfWork = start;
            LastService = service;

            if (totalKms < 0)
                throw new BusException("Negative distance of drive is invalid.");
            if (totalKms < kmsSinceFuel)
                kmsSinceFuel = 0;
            if (totalKms < kmsSinceService)
                kmsSinceService = 0;

            TotalKms = totalKms;
            KmsSinceFuel = kmsSinceFuel;
            KmsSinceService = kmsSinceService;

            LicensePlate = setLicensePlate(licensePlate);
            Status = setState();

            setCanBeFueled();
            setCanBeServiced();

            Time = "";
        }

        /// <summary>
        /// set the license plate number
        /// throw exceptions if the input is wrong
        /// </summary>
        /// <param name="value">string of license plate number</param>
        /// <returns>license plate number</returns>
        private string setLicensePlate(string value)
        {
            if (value.Length < 7 || value.Length > 8) // wrong length
                throw new BusException("Wrong length of license plate number.");

            int num = int.Parse(value);

            string tmp = Convert.ToString(num);

            if (StartOfWork.Year < 2018 && tmp.Length == 7) // 7 digits
            {
                tmp = tmp.Insert(2, "-");
                tmp = tmp.Insert(6, "-");
            }

            else if (StartOfWork.Year > 2018 && tmp.Length == 8) // 8 digits
            {
                tmp = tmp.Insert(3, "-");
                tmp = tmp.Insert(6, "-");
            }

            else // length does not fit the year
                throw new BusException("Length of license plate number does not fit the year.");

            return tmp;
        }

        /// <summary>
        /// set the bus status: canDrive, cannotDrive, driving, gettingFueled, gettingServiced
        /// </summary>
        /// <returns>bus status</returns>
        public State setState()
        {
            TimeSpan timeSinceLastTreat = DateTime.Now - LastService;
            if (timeSinceLastTreat.TotalDays >= 365 || KmsSinceService >= 20000 || KmsSinceFuel >= 1200)
                return State.cannotDrive;
            return State.canDrive;
        }

        /// <summary>
        /// check if the bus can be fueled
        /// </summary>
        public void setCanBeFueled()
        {
            if (KmsSinceFuel >= 800 && (Status == State.canDrive || Status == State.cannotDrive))
                CanBeFueled = true;
            else
                CanBeFueled = false;
        }

        /// <summary>
        /// check if the bus can be serviced
        /// </summary>
        public void setCanBeServiced()
        {
            if ((KmsSinceService >= 19500 || (DateTime.Now - LastService).TotalDays >= 350) && (Status == State.canDrive || Status == State.cannotDrive))
                CanBeServiced = true;
            else
                CanBeServiced = false;
        }

        /// <summary>
        /// fuel the bus
        /// each refueling takes 2 real hours = 12 unreal seconds
        /// </summary>
        public void fuel()
        {
            Status = State.gettingFueled;
            setCanBeServiced();
            setCanBeFueled();

            List<object> parameters = new List<object>();
            parameters.Add(12);
            parameters.Add(this);
            parameters.Add((float)-1); // mark refueling

            //new MainWindow().worker.RunWorkerAsync(parameters);
        }

        /// <summary>
        /// service the bus
        /// each service takes 24 real hours = 144 unreal seconds 
        /// </summary>
        public void service()
        {
            Status = State.gettingServiced;
            setCanBeServiced();
            setCanBeFueled();

            List<object> parameters = new List<object>();
            parameters.Add(144);
            parameters.Add(this);
            parameters.Add((float)-2); // mark service

            //new MainWindow().worker.RunWorkerAsync(parameters);
        }

        /// <summary>
        /// print the licence plate number of the bus
        /// </summary>
        /// <returns>licence plate number</returns>
        public override string ToString() { return LicensePlate; }
    }

}
