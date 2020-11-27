using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace dotNET5781_03B_6589_5401
{
    [Serializable]
    public class BasicBusExceptions : Exception
    {
        // constructors:
        public BasicBusExceptions() : base() { }
        public BasicBusExceptions(string message) : base(message) { }
        public BasicBusExceptions(string message, Exception inner) : base(message, inner) { }
        protected BasicBusExceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// print the exception
        /// </summary>
        /// <returns>name of exception + specific massage</returns>
        override public string ToString() { return "BasicBusExceptions: " + Message; }
    }
}