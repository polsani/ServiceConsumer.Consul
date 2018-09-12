using System;

namespace ServiceConsumer.Consul
{
    public class EndPointNotAvailableException : Exception
    {
        public EndPointNotAvailableException(string message) : base(message) { }
    }
}
