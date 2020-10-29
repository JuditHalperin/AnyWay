/*
Asnat Kahane 211825401
Judit Halperin 324216589
 
Exercise 1
29/10/20
This program implements the class Bus. The user can drive, fuel or teart a bus.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_01_6589_5401
{
    enum options { exit, addBus, driveBus, fuelOrTreat, kmSinceTreating }
    class Program
    {
        static List<Bus> buses = new List<Bus>();

        /// <summary>
        /// show the menu of options to user
        /// </summary>
        static void printMenu()
        {
            Console.WriteLine("Hi!");
            Console.WriteLine("Enter 1 to add a new bus.");
            Console.WriteLine("Enter 2 to drive a bus.");
            Console.WriteLine("Enter 3 to treat or fuel a bus.");
            Console.WriteLine("Enter 4 to check how much km all buses drived since their last treat.");
            Console.WriteLine("Enter 0 to exit. \n");
            Console.WriteLine("What would you like to do now?");
        }

        /// <summary>
        /// read number of option
        /// </summary>
        /// <returns>chosen option</returns>
        static options readInput(int min = 0, int max = 4)
        {
            int input;
            bool flag = int.TryParse(Console.ReadLine(), out input);

            if (flag && input >= min && input <= max)
                return (options)input;

            else
            {
                Console.WriteLine("Invalid choise.");
                return readInput(min, max);
            }
        }

        /// <summary>
        /// find bus in the list by looking for its id
        /// </summary>
        /// <param name="id">bus ID</param>
        /// <returns>correct bus or null</returns>
        static Bus findBusInList(string id)
        {
            foreach (Bus bus in buses)
                if (bus.Id == id)
                    return bus;

            return null;
        }

        /// <summary>
        /// create an ID format by adding '-' to the string 
        /// </summary>
        /// <param name="id">bus ID</param>
        /// <returns>correct ID format</returns>
        static string editFormatId(string id)
        {
            if (id.Length == 7) // 7 chars
            {
                id = id.Insert(2, "-");
                id = id.Insert(6, "-");
            }

            else if (id.Length == 8) // 8 chars
            {
                id = id.Insert(3, "-");
                id = id.Insert(6, "-");
            }

            return id;
        }

        /// <summary>
        /// add a new bus to the list
        /// </summary>
        /// <param name="date">bus's date of begining</param>
        /// <param name="id">bus ID</param>
        static void addBus(DateTime date, string id)
        {
            string msg;

            Bus bus = new Bus(date, id, out msg);

            if (msg == "The bus was successfully inserted!")
                if (findBusInList(bus.Id) == null)
                    buses.Add(bus);
                else
                    msg = "ID already exists.";

            Console.WriteLine(msg);
        }

        /// <summary>
        /// drive a bus from the list
        /// </summary>
        /// <param name="id">bus ID</param>
        static void driveBus(string id)
        {
            Bus bus = findBusInList(editFormatId(id));

            if (bus != null)
                bus.drive();

            else
                Console.WriteLine("Bus does not exist.");
        }

        static void Main(string[] args)
        {
            string id;
            int d, m, y, request;

            printMenu();
            options choise = readInput();

            while (choise != options.exit)
            {
                switch (choise)
                {
                    case options.exit:
                        break;

                    case options.addBus:

                        Console.Write("Enter id: ");
                        id = Console.ReadLine();

                        Console.WriteLine("Enter date of begining.");
                        Console.Write("\t Day: ");
                        d = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\t Month: ");
                        m = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\t Year: ");
                        y = Convert.ToInt32(Console.ReadLine());
                        if (d < 1 || d > 30 || m > 12 || m < 1 || y < 0 || y > DateTime.Now.Year)
                        {
                            Console.WriteLine("Invalid date.");
                            break;
                        }   
                        DateTime date = new DateTime(y, m, d);

                        addBus(date, id);
                        break;

                    case options.driveBus:

                        Console.Write("Enter id: ");
                        id = Console.ReadLine();

                        driveBus(id);
                        break;

                    case options.fuelOrTreat:

                        Console.Write("Enter id: ");
                        id = Console.ReadLine();
                        
                        Bus bus = findBusInList(editFormatId(id));

                        if (bus != null)
                        {
                            Console.Write("Enter 1 to fuel the bus and 2 to treat the bus: ");
                            request = (int)readInput(1,2);

                            switch (request)
                            {
                                case 1:
                                    bus.fuel();
                                    Console.WriteLine("Refueling was completed successfully.");
                                    break;

                                case 2:
                                    bus.treat();
                                    Console.WriteLine("The treatment was completed successfully.");
                                    break;

                                default:
                                    Console.WriteLine("Invalid choise.");
                                    break;
                            }
                        }

                        else
                            Console.WriteLine("Bus does not exist.");

                        break;

                    case options.kmSinceTreating:

                        if(buses.Count != 0)
                        {
                            Console.WriteLine("ID:              Km since last treat:");
                            Console.WriteLine("---------------------------------");

                            foreach (Bus tmpBus in buses)
                                Console.WriteLine("{0:11} \t {1:5}", tmpBus.Id, Convert.ToString(tmpBus.KmSinceTreated));
                        }

                        else
                            Console.WriteLine("No buses added.");
                       
                        break;
                        
                    default:

                        Console.WriteLine("Invalid choise.");
                        break;
                }

                Console.WriteLine("\nWhat would you like to do now?");
                choise = readInput();

            }

            Console.WriteLine("Bye bye!");
            Console.ReadKey();
        }
    }

}

/*

 Hi!
Enter 1 to add a new bus.
Enter 2 to drive a bus.
Enter 3 to treat or fuel a bus.
Enter 4 to check how much km all buses drived since their last treat.
Enter 0 to exit.

What would you like to do now?
1
Enter id: 1234567
Enter date of begining.
         Day: 19
         Month: 7
         Year: 2001
The bus was successfully inserted!

What would you like to do now?
1
Enter id: 12345678
Enter date of begining.
         Day: 1
         Month: 2
         Year: 2020
The bus was successfully inserted!

What would you like to do now?
2
Enter id: 12345678
Have a plasent journey! Length of driving is 646 km.

What would you like to do now?
3
Enter id: 12345678
Enter 1 to fuel the bus and 2 to treat the bus: 1
Refueling was completed successfully.

What would you like to do now?
3
Enter id: 1234567
Enter 1 to fuel the bus and 2 to treat the bus: 2
The treatment was completed successfully.

What would you like to do now?
4
ID:              Km since last treat:
---------------------------------
12-345-67        0
123-45-678       646

What would you like to do now?
0
Bye bye!

 */