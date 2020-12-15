using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BO
{
    [Serializable]
    public class BusException : Exception
    {
        // constructors:
        public BusException() : base() { }
        public BusException(string message) : base(message) { }
        public BusException(string message, Exception inner) : base(message, inner) { }
        protected BusException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// print the exception
        /// </summary>
        /// <returns>specific massage</returns>
        override public string ToString() { return Message; }
    }

}
