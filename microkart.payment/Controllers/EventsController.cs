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
        private readonly IEventBus _eventBus;
        private readonly IUserService _userService;
        private const int OrderStatusPaid = 3;
        public EventsController(
        IEventBus eventBus,
        IUserService userService,
        ILogger<EventsController> logger)
        {
            _logger = logger;
            _eventBus = eventBus;
            _userService = userService;
        }

        [HttpPost("ProcessPayment")]
        [Topic(DaprEventBus.PUBSUB_NAME, "ProcessPaymentPubSubEvent")]
        public async Task HandleProcessPayment(ProcessPaymentPubSubEvent integrationEvent)
        {
            if (integrationEvent.CorrelationId != Guid.Empty)
            {
                _logger.LogWarning("Received ProcessPaymentPubSubEvent event {@IntegrationEvent}", integrationEvent);

                var orderChngedEvent = new OrderChngedPubSubEvent(integrationEvent.OrderId,
                                                                    OrderStatusPaid,
                                                                    new List<string> { },
                                                                    integrationEvent.CorrelationId);
                _logger.LogWarning("Payment processed for Order {@Order}", integrationEvent.OrderId);
                await Task.Delay(10000);
                await _eventBus.PublishAsync(orderChngedEvent);
            }
            else
            {
                _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", integrationEvent);
            }
        }

        [HttpPost("ProcessRefund")]
        [Topic(DaprEventBus.PUBSUB_NAME, "OrderCancelledPubSubEvent")]
        public void ProcessRefund(OrderCancelledPubSubEvent integrationEvent)
        {
            if (integrationEvent.CorrelationId != Guid.Empty)
            {

                _logger.LogWarning("Order Status Changed in Payment event {@IntegrationEvent}", integrationEvent);
                // Send Refund notification event.

                // Send Order Changed event.
            }
            else
            {
                _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", integrationEvent);
            }
        }

    }
}