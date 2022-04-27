using Dapr;
using microkart.catalog.Database;
using microkart.shared.Abstraction;
using microkart.shared.Daprbuildingblocks;
using microkart.shared.Events;
using microkart.shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace microkart.catalog.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IEventBus _eventBus;
        private const int OrderStatusValidated = 2;
        private readonly CatalogDatabaseContext _catalogDatabaseContext;

        public EventsController(
        CatalogDatabaseContext catalogDatabaseContext,
        IEventBus eventBus,
        ILogger<EventsController> logger)
        {
            _logger = logger;
            _eventBus = eventBus;
            _catalogDatabaseContext = catalogDatabaseContext;
        }

        [HttpPost("HandleCheckoutInitiated")]
        [Topic(DaprEventBus.PUBSUB_NAME, "InventoryValidationPubSubEvent")]
        public async Task HandleCheckoutInitiated(InventoryValidationPubSubEvent integrationEvent)
        {
            List<string> errors = new List<string>();
            List<InventoryItems> inventoryItems = new List<InventoryItems>();
            if (integrationEvent.CorrelationId != Guid.Empty)
            {
                _logger.LogInformation("Processing InventoryValidationPubSubEvent event {@IntegrationEvent}", integrationEvent);

                foreach (var p in integrationEvent.InventoryItems)
                {
                    var product = _catalogDatabaseContext.Products.FirstOrDefault(f => f.Id == p.ProductId);
                    if(product != null)
                    {   if (product.stock >= p.Quantity)
                        {
                            product.stock -= p.Quantity;
                            inventoryItems.Add(p);
                        } 
                        else if (product.stock >= 0 && product.stock < p.Quantity)
                        {
                            inventoryItems.Add(new InventoryItems(p.ProductId,product.stock ));
                            errors.Add($"Product {product.Title} Quantity adjusted as per the availability.");
                        }                           
                        else
                            errors.Add($"Product {product.Title} is not available for requested quantity.");
                    }
                }

                await _catalogDatabaseContext.SaveChangesAsync();


                var orderChngedEvent = new InventoryVaidationResultPubSubEvent(integrationEvent.OrderId,
                                                                    OrderStatusValidated,
                                                                    inventoryItems,
                                                                    errors,
                                                                    integrationEvent.CorrelationId);
                await _eventBus.PublishAsync(orderChngedEvent);
            }
            else
            {
                _logger.LogError("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", integrationEvent);
            }
        }
    }
}