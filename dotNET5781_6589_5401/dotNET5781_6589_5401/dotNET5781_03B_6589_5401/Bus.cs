using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace dotNET5781_03B_6589_5401
{
    enum State { canDrive, cannotDrive, driving, isFueled, isTreated}
    class Bus //: INotifyPropertyChanged
    {
        static private Random rand = new Random(DateTime.Now.Millisecond);

        private string id;
        public string Id
        {
            get { return id; }
        }

        private DateTime dateOfBegining;
        public DateTime DateOfBegining
        {
            get { return dateOfBegining; }
            private set { dateOfBegining = value; }
        }

        private DateTime dateOfLastTreat;
        public DateTime DateOfLastTreat
        {
            get { return dateOfLastTreat; }
            private set { dateOfLastTreat = value; }
        }

        private float totalKm;
        public float TotalKm
        {
            get { return totalKm; }
            private set { totalKm = value; }
        }

        private float kmSinceFueled;
        public float KmSinceFueled
        {
            get { return kmSinceFueled; }
            private set { kmSinceFueled = value; }
        }

        private float kmSinceTreated;
        public float KmSinceTreated
        {
            get { return kmSinceTreated; }
            private set { kmSinceTreated = value; }
        }

        private State busState;
        public State BusState
        {
            get { return busState; }
            private set { busState = value; }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="dateBegining">date of beginning work</param>
        /// <param name="dateTreating">date of the last treat</param>
        /// <param name="id">license number</param>
        /// <param name="totalKm">total km since begining</param>
        /// <param name="kmSinceFueled">total km since the bus got fueled</param>
        /// <param name="kmSinceTreated">total km since the bus was treated</param>
        public Bus(DateTime dateBegining, DateTime dateTreating, string id, float totalKm, float kmSinceFueled, float kmSinceTreated)
        {
            DateOfBegining = dateBegining;
            DateOfLastTreat = dateTreating;
            if (dateBegining > dateTreating)
                throw new BasicBusExceptions("Invalid dates.");

            TotalKm = totalKm;
            KmSinceFueled = kmSinceFueled;
            KmSinceTreated = kmSinceTreated;
            if(totalKm < kmSinceFueled || totalKm < kmSinceTreated)
                throw new BasicBusExceptions("Invalid km.");

            checkId(id);

            BusState = getState();
        }

        /// <summary>
        /// chack if the string value is valid and match to the format.
        /// </summary>
        /// <param name="value">id</param>
        /// <returns>check if the ID is valid</returns>
        private void checkId(string value)
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
                
            id = tmp;
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
        /// <returns>return state</returns>
        public State getState()
        {
            TimeSpan timeSinceLastTreat = DateTime.Now - DateOfLastTreat;
            if (timeSinceLastTreat.TotalDays > 365 || KmSinceTreated > 20000 || KmSinceFueled > 1200)
                return State.cannotDrive;
            return State.canDrive;       
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
    }
}
