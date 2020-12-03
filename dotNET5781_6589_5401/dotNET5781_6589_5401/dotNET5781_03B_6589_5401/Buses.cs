using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace dotNET5781_03B_6589_5401
{
    public static class Buses
    {
        static private Random rand = new Random(DateTime.Now.Millisecond);

        static public ObservableCollection<Bus> buses = new ObservableCollection<Bus>();

        static private bool IsNotEmpty;

        static public bool MyProperty
        {
            get { return myVar; }
            private set { myVar = value; }
        }


        /// <summary>
        /// static constructor
        /// initialize 10 buses randomaly
        /// </summary>
        static Buses()
        {
            try
            {
                addBus(new Bus(
                        new DateTime(2019, 7, 8),
                        new DateTime(2020, 8, 5),
                        Convert.ToString(rand.Next(10000000, 20000000)),
                        rand.Next(20000, 50000),
                        rand.Next(1000, 1200),
                        rand.Next(0, 20000)));

                addBus(new Bus(
                        new DateTime(2020, 1, 1),
                        new DateTime(2020, 7, 19),
                        Convert.ToString(rand.Next(30000000, 40000000)),
                        rand.Next(20000, 50000),
                        rand.Next(0, 1200),
                        rand.Next(19500, 19900)));

                addBus(new Bus(
                        new DateTime(rand.Next(1990, 2017), rand.Next(1, 12), rand.Next(1, 28)),
                        new DateTime(DateTime.Now.Year - 1, rand.Next(1, DateTime.Now.Month), 1),
                        Convert.ToString(rand.Next(5000000, 6000000)),
                        rand.Next(20000, 50000),
                        rand.Next(0, 1200),
                        rand.Next(0, 20000)));

                for (int i = 0; i < 7; i++)
                    addBus(new Bus(
                        new DateTime(rand.Next(1990, 2017), rand.Next(1, 12), rand.Next(1, 28)),
                        new DateTime(rand.Next(DateTime.Now.Year - 1, DateTime.Now.Year + 1), rand.Next(1, 13), rand.Next(1, 29)),
                        Convert.ToString(rand.Next(1000000, 9999999)),
                        rand.Next(20000, 50000),
                        rand.Next(0, 1200),
                        rand.Next(0, 20000)));
            }

            catch (BasicBusExceptions) // in case that the random ID number already exists in the collection
            {
                ;
            }
        }

        /// <summary>
        /// add a new bus to the collection
        /// check first that its ID does not exist
        /// </summary>
        /// <param name="newBus">new bus</param>
        static public void addBus(Bus newBus)
        {
            if (!containsBus(newBus.Id))
                buses.Add(newBus);
            else
                throw new BasicBusExceptions("This license plate number already exists.");
        }

        /// <summary>
        /// test if the given ID number exists in the collection or not
        /// </summary>
        /// <param name="id">ID number</param>
        /// <returns>ID exists in the collection or not</returns>
        static private bool containsBus(string id)
        {
            foreach (Bus bus in buses)
                if (bus.Id == id)
                    return true;
            return false;
        }

        static public void removeBus(string id)
        {
            foreach (Bus bus in buses)
                if (bus.Id == id)
                {
                    buses.Remove(bus);
                    return;
                }
            throw new BasicBusExceptions("The bus does not exsit.");
        }


    }
}
