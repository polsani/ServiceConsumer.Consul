using System;
using System.Collections.Generic;

namespace ServiceConsumer.Consul
{
    public class ServersList
    {
        public IEnumerable<EndPoint> EndPoints { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
