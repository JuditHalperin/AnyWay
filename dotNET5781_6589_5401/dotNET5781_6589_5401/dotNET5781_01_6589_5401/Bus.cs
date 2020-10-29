using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_01_6589_5401
{
    class Bus
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
            private set
            {
                if (value <= DateTime.Now)
                    dateOfBegining = value;

                else
                    Console.WriteLine("Invalid date");
            }
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

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="date">date of beginning work</param>
        /// <param name="id">license number</param>
        /// <param name="msg">return massage if the process was completed successfully or not</param>
        public Bus(DateTime date, string id,out string msg)
        {
            DateOfBegining = date;
            DateOfLastTreat = date;
            checkId(id,out msg);
            TotalKm = 0;
            KmSinceFueled = 0;
            KmSinceTreated = 0;
        }

        /// <summary>
        /// chack if the string value is valid and match to the format.
        /// </summary>
        /// <param name="value">id</param>
        /// <param name="msg">return match massage</param>
        /// <returns>return if id was matched</returns>
        private bool checkId(string value, out string msg)
        {

            if (value.Length < 7 || value.Length > 8) // wrong length
            {
                msg = "Wrong length of ID number.";
                return false;
            }

            int num;
            bool flag = int.TryParse(value, out num);
            if (!flag) // convert failed
            {
                msg = "ID number should be a number.";
                return false;
            }

            if (num < 1000000 || num > 100000000) // not all digits
            {
                msg = "ID number should be consisted of digits only.";
                return false;
            }

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
            {
                msg = "Length does not fit the year.";
                return false;
            }

            id = tmp;
            msg = "The bus was successfully inserted!";
            return true;
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
                Console.WriteLine("Good drive!\nduration of the driving is {0}km.\n",km);
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
        /// <param name="msg">return if sucsses or not in massage</param>
        /// <param name="km">to this drive</param>
        /// <returns>return if sucsses or not</returns>
        public bool isValid(out string msg, float km = 0) 
        {
            TimeSpan timeSinceLastTreat = DateTime.Now - DateOfLastTreat;
            if (timeSinceLastTreat.TotalDays > 365)
            {
                msg = "the bus needs a traet because the last one was a year ago";
                return false;
            }

            if (KmSinceTreated + km > 20000)
            {
                msg = "the bus needs a traet because it drived more than 20,000 km";
                return false;
            }

            if (KmSinceFueled + km > 1200)
            {
                msg = "the bus needs to get fueled";
                return false;
            }

            msg = "everything's alright";
            return true;

        }

        /// <summary>
        /// update the km fields after drive
        /// </summary>
        /// <param name="km">additinal km that the bus drive</param>
        private void updateKm(float km)
        {
            TotalKm=TotalKm + km;
            kmSinceFueled=KmSinceFueled + km;
            kmSinceTreated=KmSinceTreated + km;
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
