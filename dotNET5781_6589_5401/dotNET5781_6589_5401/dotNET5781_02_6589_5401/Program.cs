/*
Judit Halperin - 324216589
Asnat Kahane - 211825401

Exercise 2
6/11/20
This program implements the BusStation class, the BusLine class and a collection of buses.
*/

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
        /// create 40 bus stations
        /// </summary>
        /// <returns>list of stations</returns>
        static List<BusStation> initializeBusStations()
        {
            List<BusStation> stations = new List<BusStation>();

            for (int i = 0; i < 40; i++)
                stations.Add(new BusStation());

            return stations;
        }

        /// <summary>
        /// create ten bus lines and add them to the collection
        /// </summary>
        static BusesCollection initializeBusesCollection(List<BusStation> stations)
        {
            BusesCollection buses = new BusesCollection();

            for (int i = 0, j = 0; i < 9; i++, j += 4)
                buses.addLine(stations.GetRange(j, 5));
            buses.addLine(stations.GetRange(30, 10));

            return buses;
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

        /// <summary>
        /// read a choise
        /// </summary>
        /// <returns>choise / try to read again</returns>
        static Options readInput()
        {
            Console.WriteLine("\nWhat would you like to do now?");

            int input;
            bool flag = int.TryParse(Console.ReadLine(), out input);

            if (flag) // valid
                return (Options)input;

            else // invalid
            {
                Console.WriteLine("Invalid choise.");
                return readInput();
            }
        }
    
        enum Options { addBus, addStationToBus, removeBus, removeStationFromBus, findLinesAtStation, linesToDestiny, printBuses, printStationsAndLinesStopAtThem, exit }

        static void Main(string[] args)
        {
            // constructor...

            List<BusStation> stations = initializeBusStations();
            BusesCollection buses = initializeBusesCollection(stations);

            int busLine, numberOfStations, index;
            string stationID, targetStationID;
            bool stationFoundInPath = false, stationFoundInList = false;
            BusLine bus = null;
            List<BusStation> path = new List<BusStation>();
            List<BusLine> lines = new List<BusLine>();

            printMenu();
            Options choise = readInput();

            while (choise != Options.exit)
            {
                try
                {
                    switch (choise)
                    {
                        case Options.addBus:  {
                                Console.Write("Enter number of stations in the path: ");
                                numberOfStations = int.Parse(Console.ReadLine()); // possible format exception
                                if (numberOfStations < 2)
                                    throw new BusesOrStationsExceptions("A bus has at least two stations.");
                                if (numberOfStations > stations.Count)
                                    throw new BusesOrStationsExceptions($"There are only {stations.Count} existed stations.");

                                for (int i = 1; i <= numberOfStations; i++)
                                {
                                    Console.Write($"Enter ID of station number {i} (4 digits): ");
                                    stationID = Console.ReadLine();

                                    foreach (BusStation stationInPath in path) // make sure the path does not include the station yet
                                        if (stationInPath.ID == stationID) // station already inserted
                                        {
                                            stationFoundInPath = true;
                                            Console.WriteLine("This station already exists in the path.");
                                            i--;
                                            break;
                                        }

                                    if (!stationFoundInPath)
                                    {
                                        foreach (BusStation stationInList in stations) // make sure the station exists at the stations list
                                            if (stationInList.ID == stationID) // station exists
                                            {
                                                stationFoundInList = true;
                                                path.Add(stationInList);
                                                break;
                                            }

                                        if (!stationFoundInList)
                                        {
                                            Console.WriteLine("This station does not exist.");
                                            i--;
                                        }
                                    }

                                }

                                Console.WriteLine(buses.addLine(path));

                                // get ready to the next use:
                                path.Clear();
                                stationFoundInPath = false;
                                stationFoundInList = false;

                                break;
                            }
                        case Options.addStationToBus: {
                                Console.Write("Enter bus line number: ");
                                busLine = int.Parse(Console.ReadLine()); // possible format exception
                                Console.Write("Enter station number (4 digits): ");
                                stationID = Console.ReadLine();
                                Console.Write("Enter station index in the bus path: ");
                                index = int.Parse(Console.ReadLine()); // possible format exception

                                foreach (BusStation item in stations) // find the station
                                    if(item.ID == stationID)
                                    {
                                        Console.WriteLine(buses[busLine].addStation(item, index)); ; // possible exceptions: not existed line / invalid index
                                        stationFoundInList = true;
                                        break;
                                    }

                                if(!stationFoundInList)
                                    Console.WriteLine("This station does not exist.");

                                stationFoundInList = false; // get ready to the next use

                                break;
                            }
                        case Options.removeBus: {
                                Console.Write("Enter bus line number: ");
                                busLine = int.Parse(Console.ReadLine()); // possible format exception

                                Console.WriteLine(buses.deleteLine(busLine));

                                break;
                            }
                        case Options.removeStationFromBus: {
                                Console.Write("Enter bus line number: ");
                                busLine = int.Parse(Console.ReadLine()); // possible format exception
                                Console.Write("Enter station number (4 digits): ");
                                stationID = Console.ReadLine();

                                Console.WriteLine(buses[busLine].deleteStation(stationID));

                                break;
                            }
                        case Options.findLinesAtStation: {
                                Console.Write("Enter station number (4 digits): ");
                                stationID = Console.ReadLine();

                                foreach (BusStation item in stations) // find the station
                                    if (item.ID == stationID)
                                    {
                                        stationFoundInList = true;
                                        lines = buses.findLinesThatStopAtStation(stationID);
                                        break;
                                    }

                                if (!stationFoundInList)
                                    Console.WriteLine("This station does not exist.");

                                else
                                {
                                    if (lines.Count == 0)
                                        Console.WriteLine($"No buses stop at station number {stationID}.");

                                    else
                                    {
                                        foreach (BusLine item in lines)
                                            Console.WriteLine(item);

                                        lines.Clear(); // get ready to the next use
                                    }

                                    stationFoundInList = false; // get ready to the next use
                                }
                                break;
                            }
                        case Options.linesToDestiny: {
                                Console.Write("Enter source station number (4 digits): ");
                                stationID = Console.ReadLine();                                
                                Console.Write("Enter target station number (4 digits): ");
                                targetStationID = Console.ReadLine();

                                foreach (BusLine item in buses)
                                {
                                    bus = item.subPath(stationID, targetStationID);
                                    if(bus != null)
                                        lines.Add(bus);
                                }

                                if (lines.Count == 0)
                                    Console.WriteLine($"No buses drive from station {stationID} to station {targetStationID}.");

                                else // sort and print
                                {
                                    lines.Sort(); // BusLine is comparable

                                    foreach (BusLine item in lines)
                                        Console.WriteLine($"Line: {-1*item.Line}.");
                                }

                                // get ready to the next use:
                                bus = null;
                                lines.Clear();
                                
                                break;
                            }
                        case Options.printBuses: {
                                if(buses.isEmpty())
                                    throw new BusesOrStationsExceptions("No buses exist.");
                               
                                foreach (BusLine item in buses)
                                    Console.WriteLine(item);

                                break;
                            }
                        case Options.printStationsAndLinesStopAtThem: {
                                foreach (BusStation station in stations)
                                {
                                    lines = buses.findLinesThatStopAtStation(station.ID);

                                    Console.Write($"Station {station.ID}: ");
                                    if (lines.Count == 0)
                                        Console.Write("No buses.");
                                    else foreach (BusLine item in lines)
                                            Console.Write($"{item.Line} ");
                                    Console.WriteLine();

                                    lines.Clear(); // get ready to the next time
                                }

                                break;
                            }
                        case Options.exit: break;
                        default: Console.WriteLine("Invalid choise."); break;
                    }
                }

                catch (BusesOrStationsExceptions ex)
                {
                    Console.WriteLine(ex);
                }
                catch (FormatException ex)
                {
                    // one of them:
                    Console.WriteLine(ex);
                    Console.WriteLine("FormatException: Invalid input format.");
                }

                finally
                {
                    choise = readInput();
                }
            }

            Console.WriteLine("Bye!");
            Console.ReadKey();
        }
    }
}
