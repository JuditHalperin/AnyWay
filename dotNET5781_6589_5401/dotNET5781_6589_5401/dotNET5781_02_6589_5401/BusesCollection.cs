using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    class BusesCollection : IEnumerable
    {
        private List<BusLine> buses; // (default constructor of List)

        /// <summary>
        /// add a new bus line to the collection
        /// </summary>
        /// <param name="firstStation">first station in path</param>
        public void addLine(List<BusStation> path)
        {
            BusLine newBus = new BusLine(path);
            buses.Add(newBus);
            Console.WriteLine($"Bus number {newBus.Line} was added successfully.");
        }
      
        /// <summary>
        /// remove a line from the collection
        /// </summary>
        /// <param name="line">the bus number</param>
        public void deleteLine(int line)
        {
            foreach (BusLine item in buses)
                if (item.Line == line)
                {
                    buses.Remove(item);
                    Console.WriteLine($"Bus number {line} was removed successfully.");
                    return;
                }
            
            throw new BusesOrStationsExceptions($"Bus number {line} does not exist.");
         }

        /// <summary>
        /// check which buses in the collection stop at the given station
        /// </summary>
        /// <param name="stationID">the station ID</param>
        /// <returns>list of buses</returns>
        public List<BusLine> findLinesThatStopAtStation(string stationID)
        {
            List<BusLine> busesStopAtStation = new List<BusLine>();

            foreach (BusLine item in buses)
                if (item.stopsAtStation(stationID))
                    busesStopAtStation.Add(item);

            return busesStopAtStation; // may be empty
        }
       
        ///// <summary>
        ///// sort the collection from the shortest duration bus to the longest one
        ///// </summary>
        ///// <returns>sorted list of buses</returns>
        //public List<BusLine> sortBusesByDuration()
        //{
        //    if (buses.Count == 0)
        //        throw new BusesOrStationsExceptions("No buses exist.");
            
        //    buses.Sort();
        //    return buses;
        //}

        /// <summary>
        /// return the bus the is located at the index
        /// </summary>
        /// <param name="index">index of the collection</param>
        /// <returns>bus at the given index</returns>
        public BusLine this [int line]
        {
            get
            {
                foreach (BusLine item in buses)
                    if (item.Line == line)
                        return item;

                throw new BusesOrStationsExceptions($"Bus line {line} does not exist.");
            }
        }

        /// <summary>
        /// test if the collection is empty
        /// </summary>
        /// <returns>empty or not</returns>
        public bool isEmpty()
        {
            return buses.Count == 0;
        }

         /// <summary>
        /// return iterator to the collection
        /// </summary>
        /// <returns>iterator to list</returns>
        public IEnumerator GetEnumerator()
        {
            foreach (BusLine item in buses)
                yield return item;
        }     
    }
}