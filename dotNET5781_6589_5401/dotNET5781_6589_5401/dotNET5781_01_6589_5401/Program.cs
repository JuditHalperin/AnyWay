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
            Console.WriteLine("Hi! \n");
            Console.WriteLine("Enter 1 to add a new bus. \n");
            Console.WriteLine("Enter 2 to choose a bus to drive. \n");
            Console.WriteLine("Enter 3 to treat a bus or fuel it. \n");
            Console.WriteLine("Enter 4 to check how much km all buses drived since their last treat. \n");
            Console.WriteLine("Enter 0 to exit. \n");
        }
        static Bus findBusInList(string id) // find bus in list by its id
        {
            foreach (Bus bus in buses)
                if (bus.Id == id)
                    return bus;

            return null;
        }
        static void addBus(DateTime date, string id) // add bus to list
        {
            string msg;

            Bus bus = new Bus(date, id, out msg);

            if (msg == "The bus was successfully inserted!")
                if (findBusInList(bus.Id) != null)
                {
                    buses.Add(bus);

                }
                else
                    msg = "this id already exists";

            Console.WriteLine(msg);
        }
        static void driveBus(string id) // if possible - drive a bus
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

            findBusInList(id).drive();
        }
        static void Main(string[] args)
        {
            printMenu();

            string id;
            int d, m, y, request;
            options choise = (options)Console.Read();

            while (choise != options.exit)
                switch (choise)
                {
                    case options.exit:
                        break;

                    case options.addBus:

                        Console.WriteLine("Enter id: ");
                        id = Console.ReadLine();

                        Console.WriteLine("Enter date of begining: \n");
                        Console.WriteLine("Day: ");
                        d = Console.Read();
                        Console.WriteLine("Month: "); 
                        m = Console.Read();
                        Console.WriteLine("Year: ");
                        y = Console.Read();                        
                        DateTime date = new DateTime(y, m, d);

                        addBus(date, id);
                        break;

                    case options.driveBus:

                        Console.WriteLine("Enter id: ");
                        id = Console.ReadLine();

                        driveBus(id);

                        break;

                    case fuelingOrTreating:

                        Console.WriteLine("Enter id: ");
                        id = Console.ReadLine();

                        Bus bus = new Bus(findBusInList(id));

                        if(bus != null)
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
                        

                }



            Console.ReadKey();
        }
    }

}
