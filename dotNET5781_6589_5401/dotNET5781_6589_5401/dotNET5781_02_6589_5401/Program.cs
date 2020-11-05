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
        /// <returns>choise</returns>
        static Options readInput()
        {
            Console.WriteLine("What would you like to do now?");

            int input = int.Parse(Console.ReadLine()); // exception: input is not int
            
            return (Options)input;
        }

        enum Options { addBus, addStationToBus, removeBus, removeStationFromBus, findLinesAtStation, linesToDestiny, printBuses, printStationsAndLinesStopAtThem, exit }
       
        static void Main(string[] args)
        {
            // not neccery sort before print?
            // use index?? use collection??

            List<BusStation> stations = initializeBusStations();
            BusesCollection buses = initializeBusesCollection(stations);

            bool flag = false;
            int busLine, numberOfStations;
            string stationID;
            List<BusStation> path = null;

            printMenu();
            Options choise = readInput();

            while(choise != Options.exit)
            {
                try
                {
                    switch (choise)
                    {
                        case Options.addBus:
                            {
                                Console.Write("Enter number of stations: ");
                                numberOfStations = int.Parse(Console.ReadLine()); // possible format exception
                                if (numberOfStations < 2)
                                    throw new BusesOrStationsExceptions("A bus has at least two stations.");
                                if(numberOfStations > stations.Count)
                                    throw new BusesOrStationsExceptions($"There are only {stations.Count} existed stations.");

                                for (int i = 1; i <= numberOfStations; i++) 
                                {
                                    Console.Write($"Enter ID of station {i} (6 digits): ");
                                    stationID = Console.ReadLine();
                                    foreach (BusStation item in stations)
                                        if (item.ID == stationID)
                                        {
                                            // לבדוק שתחנה לא הוכנסה כבר
                                            path.Add(item);
                                            flag = true;
                                        }

                                    if(!flag)
                                    {
                                        Console.WriteLine("This station does not exist. Try again: ");
                                        i--;
                                    }
                                }

                                buses.addLine(path);
                                break;
                            }
                        case Options.addStationToBus:
                            break;

                        case Options.removeBus:
                            {
                                Console.Write("Enter bus line number: ");
                                busLine = int.Parse(Console.ReadLine()); // exeption if input is not int

                                buses.deleteLine(busLine);

                                break;
                            }
                        case Options.removeStationFromBus:
                            {
                                Console.Write("Enter bus line number: ");
                                busLine = int.Parse(Console.ReadLine()); // exeption if input is not int
                                Console.Write("Enter station number: ");
                                stationID = Console.ReadLine();

                                buses[busLine].deleteStation(stationID);

                                break;
                            }

                        case Options.findLinesAtStation:
                            break;

                        case Options.linesToDestiny:
                            break;

                        case Options.printBuses:
                            Console.WriteLine(???);
                            break;

                        case Options.printStationsAndLinesStopAtThem:
                            break;

                        case Options.exit: break;

                        default:
                            Console.WriteLine("Invalid choise.");
                            break;
                    }
                }
                
                catch(BusesOrStationsExceptions ex)
                {
                    Console.WriteLine(ex);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex);
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
