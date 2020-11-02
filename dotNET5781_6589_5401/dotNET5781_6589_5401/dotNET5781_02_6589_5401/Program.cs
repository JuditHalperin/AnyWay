using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    class Program
    {
        /// <summary>
        /// create ten bus lines and add them to the collection
        /// </summary>
        static void initializeBusesCollection(BusesCollection buses)
        {
            // 10 lines...
        }

        /// <summary>
        /// print the options the user can choose
        /// </summary>
        static void printMenu()
        {
            Console.WriteLine("Hi!");
            Console.WriteLine("Enter 0 to add a new bus.");
            Console.WriteLine("Enter 1 to add a new station to a bus.");
            Console.WriteLine("Enter 2 to remove a bus.");
            Console.WriteLine("Enter 3 to remove a station to a bus.");
            Console.WriteLine("Enter 4 to find which buses stop at a specific station.");
            Console.WriteLine("Enter 5 to find which buses you can take from one station to another, sorted by journey duration.");
            Console.WriteLine("Enter 6 to print all existed buses, sorted by journey duration.");
            Console.WriteLine("Enter 7 to print all existed stations and the buses that stop at them.");
            Console.WriteLine("Enter 8 to exit.");
        }

        static Options readInput()
        {
            Console.WriteLine("What would you like to do now?");

            string input = Console.ReadLine();
            int num = int.Parse(input); // exeption: input is not int
            if (num < 0 || num > 8) // exeption: input is not between 0-8
                throw;

            return (Options)num;
        }

        enum Options { addBus, addStationToBus, removeBus, removeStationFromBus, findLinesAtStation, linesToDestiny, printBuses, printStationsAndLinesStopAtThem, exit }
       
        static void Main(string[] args)
        {
            // stations!
            // catch all posibily exeptions
            // if user can create stations - need to check if id exists
            // messages to user
            // stopsAtStation - public
            // not neccery sort before print?
            // use index?? use collection??
            // print: what about double??

            BusesCollection buses = new BusesCollection();

            initializeBusesCollection(buses);

            printMenu();
            Options choise = readInput();

            while(choise != Options.exit)
            {
                switch(choise)
                {
                    case Options.addBus:
                        break;

                    case Options.addStationToBus:
                        break;

                    case Options.removeBus:
                        break;

                    case Options.removeStationFromBus:
                        break;

                    case Options.findLinesAtStation:
                        break;

                    case Options.linesToDestiny:
                        break;

                    case Options.printBuses:
                        Console.WriteLine(???);
                        break;

                    case Options.printStationsAndLinesStopAtThem:
                        break;

                    case Options.exit:
                        break;
                }

                choise = readInput();
            }

            Console.WriteLine("Bye!");
            Console.ReadKey();
        }
    }
}
