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
                dateTreating = DateTime.Now;
            if (dateBegining > dateTreating)
                dateBegining = dateTreating;

            DateOfBegining = dateBegining;
            DateOfLastTreat = dateTreating;

            if (totalKm < 0)
                throw new BasicBusExceptions("Negative length is invalid.");
            if (totalKm < kmSinceFueled)
                kmSinceFueled = 0;
            if (totalKm < kmSinceTreated)
                kmSinceTreated = 0;

            TotalKm = totalKm;
            KmSinceFueled = kmSinceFueled;
            KmSinceTreated = kmSinceTreated;

            Id = setId(id);
            Status = setState();
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
                throw new BasicBusExceptions("Wrong length of ID number.");

            int num = int.Parse(value);

            if (num < 1000000 || num > 100000000) // not all digits
                throw new BasicBusExceptions("ID number should be consisted of digits only.");

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
                throw new BasicBusExceptions("Length ID number does not fit the year.");

            return tmp;
        }

        /// <summary>
        /// set the bus status: canDrive, cannotDrive, driving, gettingFueled, gettingTreated
        /// </summary>
        /// <returns>bus status</returns>
        public State setState()
        {
            TimeSpan timeSinceLastTreat = DateTime.Now - DateOfLastTreat;
            if (timeSinceLastTreat.TotalDays > 365 || KmSinceTreated > 20000 || KmSinceFueled > 1200)
                return State.cannotDrive;
            return State.canDrive;
        }

        /// <summary>
        ///  if possible - drive the bus
        /// </summary>
        public void drive()
        {
            string msg;
            float km = kmOfDrive();
            bool possible = isValid(out msg, km);

            if (possible)
            {
                updateKm(km);
                Console.WriteLine("Have a plasent journey! Length of driving is {0} km.", km);
            }

            else
                Console.WriteLine("The bus cannot drive: " + msg);
        }

        /// <summary>
        /// random length of drive
        /// </summary>
        /// <returns>random length of drive between 0 to 800</returns> 
        private float kmOfDrive() { return rand.Next(10, 800); }

        /// <summary>
        /// test if the bus can drive
        /// </summary>
        /// <param name="msg">reason for not being able to drive</param>
        /// <param name="km">length of drive</param>
        /// <returns>can drive or not</returns>
        private bool isValid(out string msg, float km = 0)
        {
            TimeSpan timeSinceLastTreat = DateTime.Now - DateOfLastTreat;
            if (timeSinceLastTreat.TotalDays > 365)
            {
                msg = "the bus needs a treat because it has not been treated for a year.";
                return false;
            }

            if (KmSinceTreated + km > 20000)
            {
                msg = "the bus needs a traet because it has drived or would drive now more than 20,000 km.";
                return false;
            }

            if (KmSinceFueled + km > 1200)
            {
                msg = $"the bus needs to get fueled before driving {km} km.";
                return false;
            }

            msg = "Everything is alright.";
            return true;
        }

        /// <summary>
        /// update the km fields after drive
        /// </summary>
        /// <param name="km">additinal km that the bus drive</param>
        private void updateKm(float km)
        {
            TotalKm = TotalKm + km;
            kmSinceFueled = KmSinceFueled + km;
            kmSinceTreated = KmSinceTreated + km;
        }

        /// <summary>
        /// reset the km since last fuel.
        /// </summary>
        public void fuel()
        {
            KmSinceFueled = 0;
        }

        /// <summary>
        /// reset the km since last treat.
        /// </summary>
        public void treat()
        {
            KmSinceTreated = 0;
            DateOfLastTreat = DateTime.Now;
        }

        /// <summary>
        /// override "ToString"
        /// </summary>
        /// <returns>ID number</returns>
        public override string ToString()
        {
            return Id;
        }

    }
}
