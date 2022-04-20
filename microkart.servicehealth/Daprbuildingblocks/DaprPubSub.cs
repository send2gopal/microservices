using Dapr.Client;
using microkart.shared.Abstraction;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microkart.shared.Daprbuildingblocks
{

    public class DaprEventBus : IEventBus
    {

        private const string PUBSUB_NAME = "pubsub";

        private readonly DaprClient _dapr;
        private readonly ILogger _logger;

        public DaprEventBus(DaprClient dapr, ILogger<DaprEventBus> logger)
        {
            _dapr = dapr;
            _logger = logger;
        }

        public async Task PublishAsync(PubSubEvent @event)
        {
            var topicName = @event.GetType().Name;

            _logger.LogInformation("Publishing event {@Event} to {PubsubName}.{TopicName}", @event, PUBSUB_NAME, topicName);

            await _dapr.PublishEventAsync(PUBSUB_NAME, topicName, (object)@event);
        }
    }
}
