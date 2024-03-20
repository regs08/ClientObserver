using System;
namespace ClientObserver.Helpers.Exceptions
{

        public class ConnectionException : Exception
        {
            public  ConnectionException(string message) : base(message) { }
        }

        public class DisconnectionException : Exception
        {
            public DisconnectionException(string message) : base(message) { }
        }

    }


