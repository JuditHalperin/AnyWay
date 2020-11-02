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
            Console.WriteLine("Enter 0 to exit.");
            Console.WriteLine("Enter 1 to add a new bus.");
            Console.WriteLine("Enter 2 to add a new station to a bus.");
            Console.WriteLine("Enter 3 to remove a bus.");
            Console.WriteLine("Enter 4 to remove a station to a bus.");
            Console.WriteLine("Enter 5 to find which buses stop at a specific station.");
            Console.WriteLine("Enter 6 to find which buses you can take from one station to another, sorted by journey duration.");
            Console.WriteLine("Enter 7 to print all existed buses, sorted by journey duration.");
            Console.WriteLine("Enter 8 to print all existed stations and the buses that stop at them.");
        }

        enum Options { exit, addBus, addStationToBus, removeBus, removeStationFromBus, findLinesAtStation, linesToDestiny, printBuses, printStationsAndLinesStopAtThem }
        static void Main(string[] args)
        {
            // catch all posibily exeptions
            // if user can create stations - need to check if id exists
            // messages to user
            // includesStation - public
            // not neccery sort before print
            // use index?? use collection??
            // print: what about double??

            BusesCollection buses = new BusesCollection();

            initializeBusesCollection(buses);
            printMenu();
            readInput();

        }
    }
}
