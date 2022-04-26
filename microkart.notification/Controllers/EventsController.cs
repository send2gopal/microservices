using Dapr;
using microkart.shared.Abstraction;
using microkart.shared.Daprbuildingblocks;
using microkart.shared.Events;
using microkart.shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace microkart.payment.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        public EventsController(
            ILogger<EventsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("OrderConfirmation")]
        [Topic(DaprEventBus.PUBSUB_NAME, "OrderConfirmationNotificationEvent")]
        public void OrderConfirmation(OrderConfirmationNotificationEvent integrationEvent)
        {
            if (integrationEvent.CorrelationId != Guid.Empty)
            {
                _logger.LogWarning("Received OrderConfirmationNotificationEvent event {@IntegrationEvent}", integrationEvent);
            }
            else
            {
                _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", integrationEvent);
            }
        }

        [HttpPost("OrderCancelled")]
        [Topic(DaprEventBus.PUBSUB_NAME, "OrderCancelledPubSubEvent")]
        public void OrderCancelled(OrderCancelledPubSubEvent integrationEvent)
        {
            if (integrationEvent.CorrelationId != Guid.Empty)
            {
                _logger.LogWarning("Received OrderConfirmationNotificationEvent event {@IntegrationEvent}", integrationEvent);
            }
            else
            {
                _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", integrationEvent);
            }
        }

        [Topic(DaprEventBus.PUBSUB_NAME, "OrderShippedNotificationEvent")]
        public void OrderShipped(OrderConfirmationNotificationEvent integrationEvent)
        {
            if (integrationEvent.CorrelationId != Guid.Empty)
            {
                _logger.LogWarning("Received OrderConfirmationNotificationEvent event {@IntegrationEvent}", integrationEvent);
            }
            else
            {
                _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", integrationEvent);
            }
        }

        [Topic(DaprEventBus.PUBSUB_NAME, "OrderDeliveredShippedNotificationEvent")]
        public void OrderDelivered(OrderConfirmationNotificationEvent integrationEvent)
        {
            if (integrationEvent.CorrelationId != Guid.Empty)
            {
                _logger.LogWarning("Received OrderConfirmationNotificationEvent event {@IntegrationEvent}", integrationEvent);
            }
            else
            {
                _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", integrationEvent);
            }
        }
    }
}