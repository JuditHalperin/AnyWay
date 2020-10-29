/*
Asnat Kahane 211825401
Judit Halperin 324216589
 
Exercise 1
29/10/20
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
        /// show menu of options to user
        /// </summary>
        static void printMenu()
        {
            Console.WriteLine("Hi!");
            Console.WriteLine("Enter 1 to add a new bus.");
            Console.WriteLine("Enter 2 to choose a bus to drive.");
            Console.WriteLine("Enter 3 to treat a bus or fuel it.");
            Console.WriteLine("Enter 4 to check how much km all buses drived since their last treat.");
            Console.WriteLine("Enter 0 to exit. \n");
        }

        /// <summary>
        /// read number of option
        /// </summary>
        /// <returns>chosen option</returns>
        static options readInput()
        {
            Console.WriteLine("What would you like to do now?");

            int input;
            bool flag = int.TryParse(Console.ReadLine(), out input);

            if (flag)
                return (options)input;

            else
            {
                Console.WriteLine("Invalid choise.");
                return readInput();
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

            Console.WriteLine(msg + '\n');
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
                        Console.Write("Day: ");
                        d = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Month: ");
                        m = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Year: ");
                        y = Convert.ToInt32(Console.ReadLine());
                        if (d < 1 || d > 30 || m > 12 || m < 1)
                        {
                            Console.WriteLine("numbers isn't valid");
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
                            request = Convert.ToInt32(Console.ReadLine());
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

                        Console.WriteLine("id:          km since last treat:");
                        Console.WriteLine("---------------------------------");

                        foreach (Bus tmpBus in buses)
                            Console.WriteLine("{0:11} \t\t {1:5}", tmpBus.Id, Convert.ToString(tmpBus.KmSinceTreated));

                        break;
                        
                    default:

                        Console.WriteLine("Invalid choise.");
                        break;
                }

                choise = readInput();

            }

            Console.WriteLine("Bye bye!");
            Console.ReadKey();
        }
    }

}

/*Hi!
Enter 1 to add a new bus.
Enter 2 to choose a bus to drive.
Enter 3 to treat a bus or fuel it.
Enter 4 to check how much km all buses drived since their last treat.
Enter 0 to exit.

What would you like to do now?
1
Enter id: 12345678
Enter date of begining.
Day: 35
Month: 12
Year: 1200
numbers isn't valid
What would you like to do now?
1
Enter id: 12345678
Enter date of begining.
Day: 25
Month: 12
Year: 2019
The bus was successfully inserted!

What would you like to do now?
1
Enter id: 1472583
Enter date of begining.
Day: 1
Month: 3
Year: 2017
The bus was successfully inserted!

What would you like to do now?
1
Enter id: 8528520
Enter date of begining.
Day: 12
Month: 12
Year: 2012
The bus was successfully inserted!

What would you like to do now?
3
Enter id: 8528520
Enter 1 to fuel the bus and 2 to treat the bus: 2
The treatment was completed successfully.
What would you like to do now?
3
Enter id: 1472583
Enter 1 to fuel the bus and 2 to treat the bus: 2
The treatment was completed successfully.
What would you like to do now?
2
Enter id: 12345678
good drive!
duration of the driving is 0km.

What would you like to do now?
2
Enter id: 12345678
good drive!
duration of the driving is 581km.

What would you like to do now?
2
Enter id: 1472583
good drive!
duration of the driving is 388km.

What would you like to do now?
2
Enter id: 8528520
good drive!
duration of the driving is 648km.

What would you like to do now?
2
Enter id: 98765432
Bus does not exist.
What would you like to do now?
4
id:          km since last treat:
---------------------------------
123 - 45 - 678               581
14 - 725 - 83                388
85 - 285 - 20                648
What would you like to do now?
2
Enter id: 8528520
good drive!
duration of the driving is 478km.

What would you like to do now?
2
Enter id: 8528520
The bus cannot drive: the bus needs to get fueled
What would you like to do now?
3
Enter id: 8528520
Enter 1 to fuel the bus and 2 to treat the bus: 1
Refueling was completed successfully.
What would you like to do now?
2
Enter id: 8528520
good drive!
duration of the driving is 775km.

What would you like to do now?
4
id:          km since last treat:
---------------------------------
123 - 45 - 678               581
14 - 725 - 83                388
85 - 285 - 20                1901
What would you like to do now?
0
Bye bye!*/
