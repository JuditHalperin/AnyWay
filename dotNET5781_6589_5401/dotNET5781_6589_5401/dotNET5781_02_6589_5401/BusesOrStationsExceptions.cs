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
using System.Runtime.Serialization;

namespace dotNET5781_02_6589_5401
{
    [Serializable]
    class BusesOrStationsExceptions : Exception
    {
        public BusesOrStationsExceptions() : base() { }
        public BusesOrStationsExceptions(string message) : base(message) { }
        public BusesOrStationsExceptions(string message, Exception inner) : base(message, inner) { }
        protected BusesOrStationsExceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// print the exception
        /// </summary>
        /// <returns>name of exception + specufic massage</returns>
        override public string ToString()
        {
            return "BusesOrStationsExceptions: " + Message;
        }
    }
}