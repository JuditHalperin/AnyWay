/*
Judit Halperin - 324216589
Asnat Kahane - 211825401

Exercise 2
8/11/20
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
            Console.WriteLine("Enter 6 to print all existed buses.");
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
            int busLine, numberOfStations, index;
            string stationID, targetStationID;
            bool stationFoundInPath = false, stationFoundInList = false;
            BusLine bus = null;
            List<BusStation> path = new List<BusStation>();
            List<BusLine> lines = new List<BusLine>();

            List<BusStation> stations = initializeBusStations();
            BusesCollection buses = initializeBusesCollection(stations);
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

                                    // get ready to the next use:
                                    stationFoundInPath = false;
                                    stationFoundInList = false;
                                }

                                Console.WriteLine(buses.addLine(path));

                                path.Clear(); // get ready to the next use

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
                                        Console.WriteLine($"Buses that stop at station number {stationID}:");
                                        foreach (BusLine item in lines)                                        
                                            Console.WriteLine($"\t{item}");
                                        
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

                                if(stationID == targetStationID)                                
                                    throw new BusesOrStationsExceptions("Source and target stations should be different stations.");
                                
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

                                    Console.WriteLine($"Buses that drive from station {stationID} to station {targetStationID} - sorted by journey duration:");
                                    foreach (BusLine item in lines)
                                        Console.WriteLine($"\tLine: {-1 * item.Line}.");
                                }

                                // get ready to the next use:
                                bus = null;
                                lines.Clear();
                                
                                break;
                            }
                        case Options.printBuses: {
                                if(buses.isEmpty())
                                    throw new BusesOrStationsExceptions("No buses exist.");

                                Console.WriteLine("Buses in the collection:");

                                foreach (BusLine item in buses)                              
                                    Console.WriteLine($"\t{item}");
                               
                                break;
                            }
                        case Options.printStationsAndLinesStopAtThem: {
                                Console.WriteLine("Station:\t\t\t\tBuses at station:");
                                foreach (BusStation station in stations)
                                {
                                    lines = buses.findLinesThatStopAtStation(station.ID);

                                    Console.Write(station + "\t\t");

                                    if (lines.Count == 0)
                                        Console.Write("No buses");

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
                catch (FormatException)
                {
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

/*

Hi!
Enter 0 to add a new bus.
Enter 1 to add a new station to a bus.
Enter 2 to remove a bus.
Enter 3 to remove a station to a bus.
Enter 4 to find which buses stop at a specific station.
Enter 5 to find which buses you can take from one station to another, sorted by journey duration.
Enter 6 to print all existed buses.
Enter 7 to print all existed stations and the buses that stop at them.
Enter 8 to exit.

What would you like to do now?
0
Enter number of stations in the path: 3
Enter ID of station number 1 (4 digits): 1020
Enter ID of station number 2 (4 digits): 1030
Enter ID of station number 3 (4 digits): 1031
Bus number 11 was added successfully.

What would you like to do now?
1
Enter bus line number: 6
Enter station number (4 digits): 1030
Enter station index in the bus path: 4
Station number 1030 was added successfully to index 4 in the path of bus number 6.

What would you like to do now?
2
Enter bus line number: 2
Bus number 2 was removed successfully.

What would you like to do now?
3
Enter bus line number: 1
Enter station number (4 digits): 1002
Station number 1002 was removed successfully from the path of bus number 1.

What would you like to do now?
4
Enter station number (4 digits): 1020
Buses that stop at station number 1020:
        Line: 5.    Region: South.    Stations: 1016 -> 1017 -> 1018 -> 1019 -> 1020
        Line: 6.    Region: South.    Stations: 1020 -> 1021 -> 1022 -> 1030 -> 1023 -> 1024
        Line: 11.    Region: Center.    Stations: 1020 -> 1030 -> 1031

What would you like to do now?
5
Enter source station number (4 digits): 1020
Enter target station number (4 digits): 1030
Buses that drive from station 1020 to station 1030 - sorted by journey duration:
        Line: 11.
        Line: 6.

What would you like to do now?
6
Buses in the collection:
        Line: 1.    Region: South.    Stations: 1000 -> 1001 -> 1003 -> 1004
        Line: 3.    Region: General.    Stations: 1008 -> 1009 -> 1010 -> 1011 -> 1012
        Line: 4.    Region: Center.    Stations: 1012 -> 1013 -> 1014 -> 1015 -> 1016
        Line: 5.    Region: South.    Stations: 1016 -> 1017 -> 1018 -> 1019 -> 1020
        Line: 6.    Region: South.    Stations: 1020 -> 1021 -> 1022 -> 1030 -> 1023 -> 1024
        Line: 7.    Region: Center.    Stations: 1024 -> 1025 -> 1026 -> 1027 -> 1028
        Line: 8.    Region: North.    Stations: 1028 -> 1029 -> 1030 -> 1031 -> 1032
        Line: 9.    Region: Center.    Stations: 1032 -> 1033 -> 1034 -> 1035 -> 1036
        Line: 10.    Region: South.    Stations: 1030 -> 1031 -> 1032 -> 1033 -> 1034 -> 1035 -> 1036 -> 1037 -> 1038 -> 1039
        Line: 11.    Region: Center.    Stations: 1020 -> 1030 -> 1031

What would you like to do now?
7
Station:                                Buses at station:
1000 (32.983716°N, 34.831048°E)         1
1001 (32.634367°N, 34.341238°E)         1
1002 (31.638451°N, 34.972832°E)         No buses
1003 (31.983605°N, 34.544233°E)         1
1004 (33.064246°N, 35.43904°E)          1
1005 (31.676441°N, 34.720292°E)         No buses
1006 (31.829236°N, 35.049872°E)         No buses
1007 (31.849699°N, 34.676574°E)         No buses
1008 (32.98874°N, 34.582051°E)          3
1009 (33.09989°N, 34.697438°E)          3
1010 (33.011099°N, 35.188286°E)         3
1011 (31.878983°N, 35.424355°E)         3
1012 (33.296606°N, 34.594005°E)         3 4
1013 (32.259899°N, 35.200501°E)         4
1014 (31.908706°N, 34.424373°E)         4
1015 (31.127566°N, 34.915852°E)         4
1016 (31.13157°N, 34.684558°E)          4 5
1017 (32.084301°N, 34.722096°E)         5
1018 (32.021845°N, 34.802164°E)         5
1019 (32.72271°N, 35.025336°E)          5
1020 (32.158502°N, 34.650816°E)         5 6 11
1021 (32.67731°N, 34.808295°E)          6
1022 (31.500847°N, 35.31282°E)          6
1023 (31.269568°N, 34.945831°E)         6
1024 (31.700077°N, 35.454244°E)         6 7
1025 (33.123238°N, 34.822398°E)         7
1026 (31.262736°N, 34.987038°E)         7
1027 (32.607058°N, 34.446695°E)         7
1028 (31.138859°N, 35.228358°E)         7 8
1029 (31.082435°N, 34.339099°E)         8
1030 (31.029695°N, 35.112683°E)         6 8 10 11
1031 (32.859408°N, 35.252624°E)         8 10 11
1032 (33.055594°N, 35.237072°E)         8 9 10
1033 (31.67399°N, 34.348086°E)          9 10
1034 (31.352953°N, 34.321224°E)         9 10
1035 (32.999922°N, 34.835439°E)         9 10
1036 (32.117887°N, 34.670258°E)         9 10
1037 (32.903254°N, 34.998452°E)         10
1038 (31.025239°N, 35.450304°E)         10
1039 (32.654166°N, 34.485409°E)         10

What would you like to do now?
8
Bye!

 */