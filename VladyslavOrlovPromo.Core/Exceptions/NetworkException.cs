using System;

namespace VladyslavOrlovPromo.Core.Exceptions
{
    [Serializable]
    public class NetworkException : Exception
    {
        public int StatusCode { get; set; }

        public NetworkException() : base() { }

        public NetworkException(string message) : base(message) { }

        public NetworkException(string message, Exception innerException) : base(message, innerException) { }
    }
}
