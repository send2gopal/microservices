using microkart.basket.Database;
using microkart.basket.Models;
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
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly BasketDatabaseContext basketDatabaseContext;
        private readonly IEventBus _eventBus;
        private readonly IUserService _userService;

        public BasketController(
            IEventBus eventBus,
            IUserService userService,
            BasketDatabaseContext context,
            ILogger<BasketController> logger)
        {
            _logger = logger;
            basketDatabaseContext = context;
            _eventBus = eventBus;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CartRequest), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartRequest>> GetBasketAsync()
        {
            var userId = _userService.GetUserIdentity();
            var basket = basketDatabaseContext.Carts.Include(e => e.Items).FirstOrDefault(c => c.UserId == userId && c.IsActive);
            if (basket != null)
            {
                var cart = new CartRequest
                {
                    IsActive = basket.IsActive,
                    Items = basket.Items.Select(c => new CartRequestItem
                    {
                        ProductId = c.ProductId,
                        ProductName = c.ProductName,
                        UnitPrice = c.UnitPrice,
                        Quantity = c.Quantity,
                        ProductImageUrl = c.ProductImageUrl
                    }).ToList()
                };
                return Ok(await Task.FromResult(cart));
            }
            return Ok(await Task.FromResult(new CartRequest()));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CartRequest), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CartRequest>> UpdateCartAsync([FromBody] CartRequest cartRequest)
        {
            var userId = _userService.GetUserIdentity();

            var cart = await basketDatabaseContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId && c.IsActive);

            var cartItems = cartRequest.Items.Select(c => new CartItem
            {
                ProductId = c.ProductId,
                ProductName = c.ProductName,
                UnitPrice = c.UnitPrice,
                Quantity = c.Quantity,
                ProductImageUrl = c.ProductImageUrl,
                CreatedBy = userId,
                UpdatedBy = userId,
                CraetedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            }).ToList();

            if (cart is not null)
            {
                cart.Items = cartItems;

            }
            else
            {
                basketDatabaseContext.Carts.Add(new Cart
                {
                    IsActive = true,
                    Items = cartItems,
                    UserId = userId,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    CraetedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                });
            }
 
            basketDatabaseContext.SaveChanges();
            return Ok(cartRequest);
        }

        [HttpPost("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody] CheckoutCart cartCheckout, [FromHeader(Name = "x-correlation-id")] string correlationId)
        {
            var userId = _userService.GetUserIdentity();

            var cart = await basketDatabaseContext.Carts.Include(c=> c.Items).FirstOrDefaultAsync(c => c.UserId == userId && c.IsActive);
            if (cart == null)
            {
                return BadRequest();
            }

            var eventRequestId = Guid.TryParse(correlationId, out Guid parsedRequestId)
                ? parsedRequestId : Guid.NewGuid();

            var eventMessage = new CheckoutInitiatedPubSubEvent(
                Guid.Parse(userId),
                cartCheckout.UserEmail,
                cartCheckout.City,
                cartCheckout.Street,
                cartCheckout.State,
                cartCheckout.ZipCode,
                cartCheckout.AptOrUnit,
                cartCheckout.Country,
                cartCheckout.CardNumber,
                cartCheckout.CardHolderName,
                cartCheckout.CardExpiration,
                cartCheckout.CardSecurityCode,
                eventRequestId,
                new CartRequest
                {
                    IsActive = cart.IsActive,
                    Items = cart.Items.Select(c => new CartRequestItem
                    {
                        ProductId = c.ProductId,
                        ProductName = c.ProductName,
                        UnitPrice = c.UnitPrice,
                        Quantity = c.Quantity,
                        ProductImageUrl = c.ProductImageUrl
                    }).ToList()
                });

            await _eventBus.PublishAsync(eventMessage);

            return Accepted();
        }
    }
}