using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BO
{
    [Serializable]
    public class BusException : Exception
    {
        public BusException() : base() { }
        public BusException(string message) : base(message) { }
        public BusException(string message, Exception inner) : base(message, inner) { }
        protected BusException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class LineException : Exception
    {
        public LineException() : base() { }
        public LineException(string message) : base(message) { }
        public LineException(string message, Exception inner) : base(message, inner) { }
        protected LineException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class StationException : Exception
    {
        public StationException() : base() { }
        public StationException(string message) : base(message) { }
        public StationException(string message, Exception inner) : base(message, inner) { }
        protected StationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class UserException : Exception
    {
        public UserException() : base() { }
        public UserException(string message) : base(message) { }
        public UserException(string message, Exception inner) : base(message, inner) { }
        protected UserException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class TripException : Exception
    {
        public TripException() : base() { }
        public TripException(string message) : base(message) { }
        public TripException(string message, Exception inner) : base(message, inner) { }
        protected TripException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
