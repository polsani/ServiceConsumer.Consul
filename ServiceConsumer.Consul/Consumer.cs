using System;
using System.Timers;
using System.Linq;
using System.Net.Http;
using Consul;

namespace ServiceConsumer.Consul
{
    public class Consumer
    {
        private readonly ServersList _serversList = new ServersList();
        private readonly ConsulClient _consulClient;
        private readonly ConsumerConfig _consumerConfig;

        public Consumer(ConsumerConfig config)
        {
            _consumerConfig = config;
            _consulClient = new ConsulClient(x =>
            {
                x.Address = new Uri(config.ServiceUrl);
            });

            UpdateCache();

            var updateCacheTimer = new Timer
            {
                Interval = config.CacheUpdateInterval
            };

            updateCacheTimer.Elapsed += UpdateCache;
            updateCacheTimer.Start();
        }

        private void UpdateCache(object sender, ElapsedEventArgs e)
        {
            UpdateCache();
        }

        public HttpClient GetHttpClient()
        {
            var endpoint = GetAvailableEndPoint();

            var client = new HttpClient { BaseAddress = new Uri($"http://{endpoint.Url}:{endpoint.Port}") };

            _consumerConfig.HttpHeaders.ForEach(x => { client.DefaultRequestHeaders.Add(x.Name, x.Value); });

            return client;
        }

        private EndPoint GetAvailableEndPoint()
        {
            if(!_serversList.EndPoints.Any())
                throw new EndPointNotAvailableException("Discovery server response is invalid or empty");

            return _serversList.EndPoints.First();
        }

        private void UpdateCache()
        {
            var serviceList = _consulClient.Agent.Services().Result.Response
                .Where(x => x.Value.Service.Equals(_consumerConfig.ServiceName))
                .Select(x => new EndPoint
                {
                    Port = x.Value.Port,
                    Url = x.Value.Address
                });

            _serversList.EndPoints = serviceList.ToList();
            _serversList.UpdateTime = DateTime.Now;
        }
    }
}
