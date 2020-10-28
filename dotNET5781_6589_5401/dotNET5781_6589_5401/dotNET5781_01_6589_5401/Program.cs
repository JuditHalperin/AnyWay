using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_01_6589_5401
{
    enum options { exit, addBus, chooseBus, fuelingOrTreating, kmSinceTreating }
    class Program
    {
        static void printMenu() // print menu to user
        {
            Console.WriteLine("Hi! \n");
            Console.WriteLine("Enter 1 to add a new bus. \n");
            Console.WriteLine("Enter 2 to choose a bus to drive. \n");
            Console.WriteLine("Enter 3 to treat a bus or fuel it. \n");
            Console.WriteLine("Enter 4 to check how much km all buses drived since their last treat. \n");
            Console.WriteLine("Enter 0 to exit. \n");
        }
        static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus>();

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

                        Bus bus = new Bus(date, id);
                        buses.Add(bus);

                        break;
                }



            Console.ReadKey();
        }
    }

}
