using AutoMapper;
using microkart.order.Database;
using microkart.order.Models;
using microkart.shared.Abstraction;
using microkart.shared.Events;
using microkart.shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace microkart.basket.Controllers
{

    [Route("api/[controller]")]
    [Authorize(Policy = "ApiScope")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderDatabaseContext orderContext;
        private readonly IEventBus _eventBus;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public OrderController(
            IEventBus eventBus,
            IUserService userService,
            OrderDatabaseContext context,
            IMapper mapper,
            ILogger<OrderController> logger)
        {
            _logger = logger;
            orderContext = context;
            _eventBus = eventBus;
            _userService = userService;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<OrderResponse>> GetOrdersAsync()
        {
            var userId = _userService.GetUserIdentity();
            var orders = await orderContext.Orders.Include(e => e.Items).Where(c => userId.ToLower() == c.UserId.ToString().ToLower())
                .ToListAsync();
            var result = new List<OrderResponse>();
            if (orders.Any())
            {
                result = _mapper.Map<List<OrderResponse>>(orders);
            }
            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResponse>> GetOrderAsync([FromRoute] int id)
        {
            var userId = _userService.GetUserIdentity();
            var order = await orderContext.Orders.Include(e => e.Items).FirstOrDefaultAsync(c => userId.ToLower() == c.UserId.ToString().ToLower() && c.Id == id);
            if (order != null)
            {
                return Ok(_mapper.Map<OrderResponse>(order));
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResponse>> CancelOrder([FromRoute] int id, [FromHeader(Name = "x-correlation-id")] string correlationId)
        {
            var userId = _userService.GetUserIdentity();
            var order = await orderContext.Orders.Include(e => e.Items).FirstOrDefaultAsync(c => userId.ToLower() == c.UserId.ToString().ToLower() && c.Id == id);
            if (order != null)
            {
                order.OrderStatus = OrderStatus.Cancelled.Id;
                await orderContext.SaveChangesAsync();
                var orderCancelled = new OrderCancelledPubSubEvent(
                        order.UserId,
                        order.Id,
                        order.Enail,
                        order.PaymentInformation.CardNumber,
                        order.PaymentInformation.CardHolderName,
                        order.PaymentInformation.CardExpiration,
                        order.PaymentInformation.CardSecurityNumber,
                        order.PaymentInformation.Amount,
                        order.PaymentInformation.PaymentReferenceNumber,
                        Guid.Parse(correlationId),
                        new CartRequest
                        {
                            Items = order.Items.Select(c => new CartRequestItem
                            {
                                ProductId = c.ProductId,
                                ProductName = c.ProductName,
                                UnitPrice = c.UnitPrice,
                                Quantity = c.Quantity,
                                ProductImageUrl = c.ProductImageUrl
                            }).ToList()
                        });
                await _eventBus.PublishAsync(orderCancelled);
                return Accepted();
            }
            return NotFound();
        }

        [HttpGet("shipping-addresses")]
        [ProducesResponseType(typeof(ShippingAddressResponse), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<ShippingAddressResponse>> GetShippingAddressesAsync()
        {
            var userId = _userService.GetUserIdentity();
            var addresses = await orderContext.ShippingAddresses.Where(c => userId.ToLower() == c.UserId.ToString().ToLower())
                .DistinctBy(comparer => comparer.AptOrUnit)
                .ToListAsync();
            var result = new List<ShippingAddressResponse>();
            if (addresses.Any())
            {
                result = _mapper.Map<List<ShippingAddressResponse>>(addresses);
            }
            return result;
        }

        [HttpGet("payment-informations")]
        [ProducesResponseType(typeof(PaymentInformationResponse), (int)HttpStatusCode.OK)]
        public async Task<IEnumerable<PaymentInformationResponse>> GetPaymentInformationsAsync()
        {
            var userId = _userService.GetUserIdentity();
            var addresses = await orderContext.PaymentInformations.Where(c => userId.ToLower() == c.UserId.ToString().ToLower())
                .DistinctBy(comparer => comparer.CardNumber)
                .ToListAsync();
            var result = new List<PaymentInformationResponse>();
            if (addresses.Any())
            {
                result = _mapper.Map<List<PaymentInformationResponse>>(addresses);
            }
            return result;
        }

        [HttpGet("{id}/ship")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResponse>> ShipOrder([FromRoute] int id, [FromHeader(Name = "x-correlation-id")] string correlationId)
        {
            var userId = _userService.GetUserIdentity();
            var order = await orderContext.Orders.Include(e => e.Items).FirstOrDefaultAsync(c => c.Id == id);
            if (order != null)
            {
                var orderChngedEvent = new OrderChngedPubSubEvent(order.Id,
                                                                    4, //Shipped
                                                                    new List<string> { },
                                                                    Guid.Parse(correlationId));
                await _eventBus.PublishAsync(orderChngedEvent);
                return Accepted();
            }
            return NotFound();
        }

        [HttpGet("{id}/deliver")]
        [ProducesResponseType(typeof(OrderResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResponse>> DeliverOrder([FromRoute] int id, [FromHeader(Name = "x-correlation-id")] string correlationId)
        {
            var userId = _userService.GetUserIdentity();
            var order = await orderContext.Orders.Include(e => e.Items).FirstOrDefaultAsync(c => c.Id == id);
            if (order != null)
            {
                var orderChngedEvent = new OrderChngedPubSubEvent(order.Id,
                                                                    7, //Shipped
                                                                    new List<string> { },
                                                                    Guid.Parse(correlationId));
                await _eventBus.PublishAsync(orderChngedEvent);
                return Accepted();
            }
            return NotFound();
        }
    }
}