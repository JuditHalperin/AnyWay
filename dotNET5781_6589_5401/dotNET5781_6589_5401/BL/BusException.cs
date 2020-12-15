using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class BusExceptions : Exception
    {
        // constructors:
        public BusExceptions() : base() { }
        public BusExceptions(string message) : base(message) { }
        public BusExceptions(string message, Exception inner) : base(message, inner) { }
        protected BusExceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// print the exception
        /// </summary>
        /// <returns>specific massage</returns>
        override public string ToString() { return Message; }
    }
}