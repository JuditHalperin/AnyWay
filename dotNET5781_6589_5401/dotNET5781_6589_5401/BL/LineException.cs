using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class LineException : Exception
    {
        // constructors:
        public LineException() : base() { }
        public LineException(string message) : base(message) { }
        public LineException(string message, Exception inner) : base(message, inner) { }
        protected LineException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary>
        /// print the exception
        /// </summary>
        /// <returns>specific massage</returns>
        override public string ToString() { return Message; }
    }
}
