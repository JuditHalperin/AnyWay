using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class StationException : Exception
    {
        // constructors:
        public StationException() : base() { }
        public StationException(string message) : base(message) { }
        public StationException(string message, Exception inner) : base(message, inner) { }
        protected StationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// print the exception
        /// </summary>
        /// <returns>specific massage</returns>
        override public string ToString() { return Message; }
    }
}

