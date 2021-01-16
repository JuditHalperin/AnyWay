using System;
using System.Runtime.Serialization;

namespace PO
{
    [Serializable]
    public class InvalidInputException : Exception
    {
        public InvalidInputException() : base() { }
        public InvalidInputException(string message) : base(message) { }
        public InvalidInputException(string message, Exception inner) : base(message, inner) { }
        protected InvalidInputException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}