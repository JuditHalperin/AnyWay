using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace dotNET5781_03B_6589_5401
{
    public enum State { canDrive, cannotDrive, driving, gettingFueled, gettingTreated }
    public class Bus : INotifyPropertyChanged
    {
        static private Random rand = new Random(DateTime.Now.Millisecond);

        public event PropertyChangedEventHandler PropertyChanged;

        #region
        private string id; public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private DateTime dateOfBegining; public DateTime DateOfBegining
        {
            get { return dateOfBegining; }
            private set { dateOfBegining = value; }
        }
        private DateTime dateOfLastTreat; public DateTime DateOfLastTreat
        {
            get { return dateOfLastTreat; }
            private set
            {
                dateOfLastTreat = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("DateOfLastTreat"));
            }
        }
        private float totalKm; public float TotalKm
        {
            get { return totalKm; }
            private set
            {
                totalKm = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalKm"));
            }
        }
        private float kmSinceFueled; public float KmSinceFueled
        {
            get { return kmSinceFueled; }
            private set
            {
                kmSinceFueled = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("KmSinceFueled"));
            }
        }
        private float kmSinceTreated; public float KmSinceTreated
        {
            get { return kmSinceTreated; }
            private set
            {
                kmSinceTreated = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("KmSinceTreated"));
            }
        }
        private State status; public State Status
        {
            get { return status; }
            private set
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
            private set
            {
                time = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Time"));
            }
        }

        #endregion

        /// <summary>
        /// constructor
        /// change dates and length if they are wrong
        /// </summary>
        /// <param name="dateBegining">date of beginning</param>
        /// <param name="dateTreating">date of last treat</param>
        /// <param name="id">ID number</param>
        /// <param name="totalKm">total km since begining</param>
        /// <param name="kmSinceFueled">total km since the bus got fueled</param>
        /// <param name="kmSinceTreated">total km since the bus was treated</param>
        public Bus(DateTime dateBegining, DateTime dateTreating, string id, float totalKm, float kmSinceFueled = 0, float kmSinceTreated = 0)
        {
            if (dateTreating > DateTime.Now)
                dateTreating = DateTime.Now.Date;
            if (dateBegining > dateTreating)
                dateBegining = dateTreating;

            DateOfBegining = dateBegining;
            DateOfLastTreat = dateTreating;

            if (totalKm < 0)
                throw new BasicBusExceptions("Negative distance of drive is invalid.");
            if (totalKm < kmSinceFueled)
                kmSinceFueled = 0;
            if (totalKm < kmSinceTreated)
                kmSinceTreated = 0;

            TotalKm = totalKm;
            KmSinceFueled = kmSinceFueled;
            KmSinceTreated = kmSinceTreated;

            Id = setId(id);
            Status = setState();

            setCanBeFueled();
            setCanBeServiced();
        }

        /// <summary>
        /// set the ID number
        /// throw exceptions if the input is wrong
        /// </summary>
        /// <param name="value">string of ID</param>
        /// <returns>ID number</returns>
        private string setId(string value)
        {
            if (value.Length < 7 || value.Length > 8) // wrong length
                throw new BasicBusExceptions("Wrong length of license plate number.");

            int num = int.Parse(value);
            
            string tmp = Convert.ToString(num);

            if (dateOfBegining.Year < 2018 && tmp.Length == 7) // 7 digits
            {
                tmp = tmp.Insert(2, "-");
                tmp = tmp.Insert(6, "-");
            }

            else if (dateOfBegining.Year > 2018 && tmp.Length == 8) // 8 digits
            {
                tmp = tmp.Insert(3, "-");
                tmp = tmp.Insert(6, "-");
            }

            else // length does not fit the year
                throw new BasicBusExceptions("Length of license plate number does not fit the year.");

            return tmp;
        }

        /// <summary>
        /// set the bus status: canDrive, cannotDrive, driving, gettingFueled, gettingTreated
        /// </summary>
        /// <returns>bus status</returns>
        public State setState()
        {
            TimeSpan timeSinceLastTreat = DateTime.Now - DateOfLastTreat;
            if (timeSinceLastTreat.TotalDays >= 365 || KmSinceTreated >= 20000 || KmSinceFueled >= 1200)
                return State.cannotDrive;
            return State.canDrive;
        }

        /// <summary>
        /// check if the bus can be fueled
        /// </summary>
        public void setCanBeFueled()
        {
            if (kmSinceFueled > 800 && (Status == State.canDrive || Status == State.cannotDrive))
                CanBeFueled = true;
            else
                CanBeFueled = false;
        }

        /// <summary>
        /// check if the bus can be serviced
        /// </summary>
        public void setCanBeServiced()
        {
            if ((kmSinceTreated > 19500 || (DateTime.Now - DateOfLastTreat).TotalDays > 350) &&  (Status == State.canDrive || Status == State.cannotDrive))
                CanBeServiced = true;
            else
                CanBeServiced = false;
        }

        /// <summary>
        ///  drive the bus
        /// </summary>
        public void drive(float km)
        {
            string msg;
            bool possible = isAbleToDriveSpecificLength(out msg, km);

            if (possible)
            {
                Status = State.driving;

                List<object> parameters = new List<object>();
                parameters.Add(144);
                parameters.Add(this);
                new MainWindow().worker.RunWorkerAsync(parameters);

                updateKm(km);
                Status = setState();
                setCanBeFueled();
                setCanBeServiced();
            }

            else
                throw new BasicBusExceptions("The bus cannot drive.\n" + msg);
        }
        
        /// <summary>
        /// test if the bus can drive the given length
        /// </summary>
        /// <param name="msg">reason for not being able to drive</param>
        /// <param name="km">length of drive</param>
        /// <returns>can drive this length or not</returns>
        private bool isAbleToDriveSpecificLength(out string msg, float km = 0)
        {
            TimeSpan timeSinceLastTreat = DateTime.Now - DateOfLastTreat;
            if (timeSinceLastTreat.TotalDays > 365)
            {
                msg = "It needs to be serviced because it has not been serviced for a year.";
                return false;
            }

            if (KmSinceTreated + km > 20000)
            {
                msg = $"It needs to be serviced before driving {km} km.";
                return false;
            }

            if (KmSinceFueled + km > 1200)
            {
                msg = $"It needs to be fueled before driving {km} km.";
                return false;
            }

            msg = "Everything is alright.";
            return true;
        }

        /// <summary>
        /// update the km fields after a drive
        /// </summary>
        /// <param name="km">additional km that the bus has drived</param>
        private void updateKm(float km)
        {
            TotalKm = TotalKm + km;
            kmSinceFueled = KmSinceFueled + km;
            kmSinceTreated = KmSinceTreated + km;
        }

        /// <summary>
        /// fuel the bus
        /// ? seconds for each fuel
        /// </summary>
        public void fuel()
        {
            Status = State.gettingFueled;
            //
            KmSinceFueled = 0;
            Status = setState();
            setCanBeFueled();
        }

        /// <summary>
        /// treat the bus
        /// ? seconds for each treat
        /// </summary>
        public void treat()
        {
            Status = State.gettingTreated;

            //

            KmSinceTreated = 0;
            DateOfLastTreat = DateTime.Now.Date;

            if (kmSinceFueled >= 1100)
                kmSinceFueled = 0;

            Status = setState();
            setCanBeServiced();
        }

        /// <summary>
        /// print the ID number of the bus
        /// override of "ToString"
        /// </summary>
        /// <returns>ID number</returns>
        public override string ToString() { return Id; }
       
    }
}
