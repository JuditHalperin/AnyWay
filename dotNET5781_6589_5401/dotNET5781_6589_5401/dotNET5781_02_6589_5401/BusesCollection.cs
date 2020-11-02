using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNET5781_02_6589_5401
{
    class BusesCollection : IEnumerable<BusLine>
    {
        List<BusLine> buses; // (default constructor of List)

        /// <summary>
        /// add a new bus line to the collection
        /// </summary>
        /// <param name="line">the bus number</param>
        /// <param name="region">area in Israel the bus travel in</param>
        /// <param name="path">path of stations the bus drive to</param>
        public void addLine(int line, int region, List<BusLineStation> path)
        {
            int counter = 0;
            BusLine sameLine = null;

            foreach (BusLine item in buses)
                if (item.Line == line)
                {
                    counter++; // count how many lines exist
                    sameLine = item;
                }

            if (counter == 0 || (counter == 1 && path.First().ID == sameLine.Path.Last().ID && path.Last().ID == sameLine.Path.First().ID))
            {
                buses.Add(new BusLine(line, region, path));
                Console.WriteLine("The bus was added successfully.");
            }

            else // this line already exists twice, or once - but the first and last stations are not exchanged
                throw;           
        }

        /// <summary>
        /// remove a line from the collection
        /// </summary>
        /// <param name="line">the bus number</param>
        public void deleteLine(int line)
        {
            int counter = 0;

            foreach (BusLine item in buses)
                if (item.Line == line)
                {
                    counter++;
                    buses.Remove(item);
                }

            if (counter == 0)
                throw;

            if(counter == 1)
                Console.WriteLine("One bus line was removed successfully.");
            else // counter == 2
                Console.WriteLine("Two bus lines were removed successfully.");
        }

        /// <summary>
        /// check which buses in the collection stop at the given station
        /// </summary>
        /// <param name="stationID">the station ID</param>
        /// <returns>list of buses</returns>
        public List<BusLine> findLinesThatStopAtStation(string stationID)
        {
            List<BusLine> busesStopAtStation = null;

            foreach (BusLine item in buses)
                if (item.stopsAtStation(stationID))
                    busesStopAtStation.Add(item);

            if (busesStopAtStation.Count == 0)
                throw;

            return busesStopAtStation;
        }
       
        /// <summary>
        /// sort the collection from the shortest duration bus to the longest one
        /// </summary>
        /// <returns>sorted list of buses</returns>
        public List<BusLine> sortBusesByDuration()
        {
            if (buses.Count == 0)
                throw ;
            
            buses.Sort();
            return buses;
        }

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

                throw; // does not exist
            }
        }

        // interface method:

         /// <summary>
        /// return iterator to the collection
        /// </summary>
        /// <returns>iterator to list</returns>
        public IEnumerator<BusLine> GetEnumerator()
        {
            foreach (BusLine item in buses)
                yield return item;
        }

        //private object current;
        //public object Current
        //{
        //    get { return current; }
        //    set { current = value; }
        //}

        //public bool MoveNext()
        //{
        //    return true;
        //}

        //public void Reset()
        //{

        //}

    }
}
