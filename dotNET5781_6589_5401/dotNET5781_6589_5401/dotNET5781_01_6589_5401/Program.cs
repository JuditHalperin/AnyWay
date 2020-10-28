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

        }
        static void addBus(DateTime date, string id) // add bus to list
        {
            if(findBusInList(id) !=)
            {
                Bus bus = new Bus(date, id);
                buses.Add(bus);
                Console.WriteLine();
            }
            else
                Console.WriteLine();
        }
        static void driveBus(string id) // if possible - drive a bus
        {
            findBusInLis(id).drive();
        }
        static void Main(string[] args)
        {
            printMenu();

            string id;
            int d, m, y;
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

                }



            Console.ReadKey();
        }
    }

}
