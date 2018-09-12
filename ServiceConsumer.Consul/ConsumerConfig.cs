using System.Collections.Generic;

namespace ServiceConsumer.Consul
{
    public class ConsumerConfig
    {
        public string ServiceUrl { get; set; }
        public string ServiceName { get; set; }
        public int CacheUpdateInterval { get; set; }
        public IEnumerable<HttpHeader> HttpHeaders { get; set; }
    }
}
