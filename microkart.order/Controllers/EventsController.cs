using Dapr;
using microkart.order.Database;
using microkart.order.Models;
using microkart.shared.Abstraction;
using microkart.shared.Daprbuildingblocks;
using microkart.shared.Events;
using microkart.shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace microkart.order.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly OrderDatabaseContext orderDatabaseContext;
        private readonly IEventBus _eventBus;

        public EventsController(
            IEventBus eventBus,
            OrderDatabaseContext context,
            ILogger<EventsController> logger)
        {
            _logger = logger;
            orderDatabaseContext = context;
            _eventBus = eventBus;
        }

        [HttpPost("UserCheckoutAccepted")]
        [Topic(DaprEventBus.PUBSUB_NAME, "CheckoutInitiatedPubSubEvent")]
        public void HandleCheckoutInitiated(CheckoutInitiatedPubSubEvent integrationEvent)
        {
            if (integrationEvent.CorrelationId != Guid.Empty)
            {
                _logger.LogWarning("Received Order processing event {@IntegrationEvent}", integrationEvent);
                var order = new OrderEntity
                {
                    UserId = integrationEvent.UserId,
                    Enail = integrationEvent.UserEmail,
                    Amount = integrationEvent.Cart.Items.Sum(s => s.Totalprice),
                    Items = integrationEvent.Cart.Items.Select(s => new OrderItem
                    {
                        ProductId = s.ProductId,
                        ProductName = s.ProductName,
                        UnitPrice = s.UnitPrice,
                        Quantity = s.Quantity,
                        ProductImageUrl = s.ProductImageUrl,
                        CreatedBy = integrationEvent.UserId.ToString(),
                        CraetedDate = DateTime.Now,
                        UpdatedBy = integrationEvent.UserId.ToString(),
                        UpdatedDate = DateTime.Now,

                    }).ToList(),

                    ShippingAddress = new ShippingAddress
                    {
                        UserId = integrationEvent.UserId,
                        AptOrUnit = integrationEvent.AptOrUnit,
                        City = integrationEvent.City,
                        Country = integrationEvent.Country,
                        State = integrationEvent.State,
                        Street = integrationEvent.Street,
                        ZipCode = integrationEvent.ZipCode,
                        CreatedBy = integrationEvent.UserId.ToString(),
                        CraetedDate = DateTime.Now,
                        UpdatedBy = integrationEvent.UserId.ToString(),
                        UpdatedDate = DateTime.Now,
                    },
                    PaymentInformation = new PaymentInformation
                    {
                        UserId = integrationEvent.UserId,
                        Amount = integrationEvent.Cart.Items.Sum(s => s.Totalprice),
                        CardExpiration = integrationEvent.CardExpiration,
                        CardHolderName = integrationEvent.CardHolderName,
                        CardNumber = integrationEvent.CardNumber,
                        CardSecurityNumber = integrationEvent.CardSecurityNumber,
                        CreatedBy = integrationEvent.UserId.ToString(),
                        CraetedDate = DateTime.Now,
                        UpdatedBy = integrationEvent.UserId.ToString(),
                        UpdatedDate = DateTime.Now,
                    },
                    OrderStatus = OrderStatus.AwaitingStockValidation.Id,
                    CreatedBy = integrationEvent.UserId.ToString(),
                    CraetedDate = DateTime.Now,
                    UpdatedBy = integrationEvent.UserId.ToString(),
                    UpdatedDate = DateTime.Now,
                };

                var entity = orderDatabaseContext.Orders.Add(order);
                orderDatabaseContext.SaveChanges();

                var inventoryValidationPubSubEvent = new InventoryValidationPubSubEvent(
                    entity.Entity.Id,
                    integrationEvent.Cart.Items.Select(s => new InventoryItems(s.ProductId, s.Quantity)).ToList(),
                    integrationEvent.CorrelationId
                    );
                _logger.LogWarning("Sending InventoryValidationPubSubEvent - {@IntegrationEvent}", inventoryValidationPubSubEvent);
                _eventBus.PublishAsync(inventoryValidationPubSubEvent);
            }
            else
            {
                _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", integrationEvent);
            }
        }

        [HttpPost("OrderChnged")]
        [Topic(DaprEventBus.PUBSUB_NAME, "OrderChngedPubSubEvent")]
        public async Task HandleStstusChanged(OrderChngedPubSubEvent integrationEvent)
        {
            var Order = orderDatabaseContext.Orders
                .FirstOrDefault(o => o.Id == integrationEvent.OrderId);

            if (integrationEvent.CorrelationId != Guid.Empty)
            {
                _logger.LogWarning("Order Status Changed event {@IntegrationEvent}", integrationEvent);
                var order = orderDatabaseContext.Orders.FirstOrDefault(o => o.Id == integrationEvent.OrderId);
                if (order == null) throw new Exception("Invalid Order ID {}");

                if (integrationEvent.Status == OrderStatus.Paid.Id)
                {
                    var orderConfirmationNotification = new OrderConfirmationNotificationEvent(
                            order.UserId,
                            order.Id,
                            order.Enail,
                            integrationEvent.CorrelationId
                        );
                    await _eventBus.PublishAsync(orderConfirmationNotification);
                }
                else if (integrationEvent.Status == OrderStatus.PaymentFailed.Id)
                {
                    var orderConfirmationNotification = new OrderConfirmationNotificationEvent(
                            order.UserId,
                            order.Id,
                            order.Enail,
                            integrationEvent.CorrelationId
                        );
                    await _eventBus.PublishAsync(orderConfirmationNotification);
                }
                else if (integrationEvent.Status == OrderStatus.Shipped.Id)
                {
                    //var orderConfirmationNotification = new OrderConfirmationNotificationEvent(
                    //        order.UserId,
                    //        order.Id,
                    //        order.Enail,
                    //        integrationEvent.CorrelationId
                    //    );
                    //await _eventBus.PublishAsync(orderConfirmationNotification);
                    _logger.LogWarning("Order Shipped - {@IntegrationEvent}", integrationEvent);
                }
                order.OrderStatus = integrationEvent.Status;

                orderDatabaseContext.SaveChanges();
            }
            else
            {
                _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", integrationEvent);
            }
        }

        [HttpPost("InventoryVaidationResponse")]
        [Topic(DaprEventBus.PUBSUB_NAME, "InventoryVaidationResultPubSubEvent")]
        public async Task InventoryVaidationResponseHandler(InventoryVaidationResultPubSubEvent integrationEvent)
        {
            var Order = orderDatabaseContext.Orders
                .FirstOrDefault(o => o.Id == integrationEvent.OrderId);

            if (integrationEvent.CorrelationId != Guid.Empty)
            {
                _logger.LogInformation("Processing InventoryVaidationResultPubSubEvent {@IntegrationEvent}", integrationEvent);
                var order = orderDatabaseContext.Orders.FirstOrDefault(o => o.Id == integrationEvent.OrderId);
                if (order == null) throw new Exception($"Invalid Order ID {integrationEvent.OrderId}");

                if (integrationEvent.errors.Any())
                {
                    foreach (var p in integrationEvent.updatedInventoryItems)
                    {
                        var orderItem = order.Items.Find(f => f.Id == p.ProductId);
                        if(orderItem != null)
                            orderItem.Quantity= p.Quantity;
                    }
                    // Send order modification notification 
                    //TBD
                }


                var processPaymentEvent = new ProcessPaymentPubSubEvent(
                        order.UserId,
                        order.Id,
                        order.PaymentInformation.CardNumber,
                        order.PaymentInformation.CardHolderName,
                        order.PaymentInformation.CardExpiration,
                        order.PaymentInformation.CardSecurityNumber,
                        order.Amount,
                        integrationEvent.CorrelationId
                        );

                await _eventBus.PublishAsync(processPaymentEvent);

                order.OrderStatus = integrationEvent.Status;

                await orderDatabaseContext.SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", integrationEvent);
            }
        }
    }
}