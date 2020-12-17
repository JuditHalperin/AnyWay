using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PO
{   
    [Serializable]
    public class InvalidInputException : Exception
    {
        // constructors:
        public InvalidInputException() : base() { }
        public InvalidInputException(string message) : base(message) { }
        public InvalidInputException(string message, Exception inner) : base(message, inner) { }
        protected InvalidInputException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// print the exception
        /// </summary>
        /// <returns>specific massage</returns>
        override public string ToString() { return Message; }
    }
}
