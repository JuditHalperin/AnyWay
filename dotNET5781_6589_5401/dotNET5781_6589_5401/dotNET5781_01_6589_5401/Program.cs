using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_01_6589_5401
{
    enum options { exit, addBus, driveBus, fuelingOrTreating, kmSinceTreating }
    class Program
    {
        static List<Bus> buses = new List<Bus>();
        static void printMenu() // print menu to user
        {
            Console.WriteLine("Hi!");
            Console.WriteLine("Enter 1 to add a new bus.");
            Console.WriteLine("Enter 2 to choose a bus to drive.");
            Console.WriteLine("Enter 3 to treat a bus or fuel it.");
            Console.WriteLine("Enter 4 to check how much km all buses drived since their last treat.");
            Console.WriteLine("Enter 0 to exit. \n");
        }
        static Bus findBusInList(string id) // find bus in list by its id
        {
           
            foreach (Bus bus in buses)
                if (bus.Id == id)
                    return bus;

            return null;
        }

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
        static void addBus(DateTime date, string id) // add bus to list
        {
            string msg;

            Bus bus = new Bus(date, id, out msg);

            if (msg == "The bus was successfully inserted!")
                if (findBusInList(bus.Id) == null)
                {
                    buses.Add(bus);

                }
                else
                    msg = "this id already exists";

            Console.WriteLine(msg + '\n');
        }
        static void driveBus(string id) // if possible - drive a bus
        {

            Bus bus=findBusInList(editFormatId(id));
            if (bus != null)
                bus.drive();
            else
                Console.WriteLine("Sorry.\nthe bus is not exist!\n");
        }
        static void Main(string[] args)
        {
            printMenu();

            string id;
            int d, m, y, request;

            Console.WriteLine("What do you want to do now?");
            int intTemp = Convert.ToInt32(Console.ReadLine());
            options choise = (options)intTemp;

            while (choise != options.exit)
            {
                switch (choise)
                {
                    case options.exit:
                        break;

                    case options.addBus:

                        Console.WriteLine("Enter id:");
                        id = Console.ReadLine();

                        Console.WriteLine("Enter date of begining:");
                        Console.WriteLine("Day: ");
                        d = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Month: ");
                        m = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Year: ");
                        y = Convert.ToInt32(Console.ReadLine());
                        DateTime date = new DateTime(y, m, d);

                        addBus(date, id);
                        break;

                    case options.driveBus:

                        Console.WriteLine("Enter id: ");
                        id = Console.ReadLine();

                        driveBus(id);
                        break;

                    case options.fuelingOrTreating:

                        Console.WriteLine("Enter id: ");
                        id = Console.ReadLine();
                        
                        Bus bus = findBusInList(editFormatId(id));

                        if (bus != null)
                        {
                            Console.WriteLine("Enter 1 to fuel the bus and 2 to treat the bus: ");
                            request = Console.Read();
                            switch (request)
                            {
                                case 1:
                                    bus.fuel();
                                    break;

                                case 2:
                                    bus.treat();
                                    break;

                                default:
                                    Console.WriteLine("Invalid choise");
                                    break;

                            }

                        }
                        else
                            Console.WriteLine("sorry, the bus isn't exist!");

                        break;

                    case options.kmSinceTreating:

                        Console.WriteLine("id:          km since last treat:");
                        Console.WriteLine("---------------------------------");

                        foreach (Bus tmpBus in buses)
                            Console.WriteLine("{0:11} \t\t {1:5}", tmpBus.Id, tmpBus.KmSinceTreated);

                        break;

                    default:

                        Console.WriteLine("Invalid number.");
                        break;
                }

                Console.WriteLine("What do you want to do now?");
                intTemp = Convert.ToInt32(Console.ReadLine());
                choise = (options)intTemp;

            }

            Console.WriteLine("Bye bye!");
            Console.ReadKey();
        }
    }

}
