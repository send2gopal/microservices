using microkart.basket.Database;
using microkart.shared.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace microkart.basket.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;
        private readonly BasketDatabaseContext basketDatabaseContext;
        private readonly IEventBus _eventBus;

        public BasketController(
            IEventBus eventBus,
            BasketDatabaseContext context,
            ILogger<BasketController> logger)
        {
            _logger = logger;
            basketDatabaseContext = context;
            _eventBus = eventBus;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> GetBasketAsync()
        {
            var userId = "gthakur";
            var basket = basketDatabaseContext.Carts.FirstOrDefault(c=> c.UserId == userId);

            return Ok(await Task.FromResult(basket) ?? new Cart(userId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Cart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Cart>> UpdateCartAsync([FromBody] Cart cartRequest)
        {
            cartRequest.UserId = "gthakur";

            var cart = await basketDatabaseContext.Carts.FirstOrDefaultAsync(c => c.UserId == cartRequest.UserId);

            if(cart is not null)
                basketDatabaseContext.Carts.Remove(cart);

            basketDatabaseContext.Carts.Add(cartRequest);
            basketDatabaseContext.SaveChanges();
            return Ok(cart);
        }

        [HttpPost("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody] CheckoutCart cartCheckout, [FromHeader(Name = "X-Correlation-Id")] string correlationId)
        {
            var userId = "gthakur";

            var cart = await basketDatabaseContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
            if (cart == null)
            {
                return BadRequest();
            }

            var eventRequestId = Guid.TryParse(correlationId, out Guid parsedRequestId)
                ? parsedRequestId : Guid.NewGuid();

            var eventMessage = new CheckoutPubSubEvent(
                userId,
                cartCheckout.UserEmail,
                cartCheckout.City,
                cartCheckout.Street,
                cartCheckout.State,
                cartCheckout.Country,
                cartCheckout.CardNumber,
                cartCheckout.CardHolderName,
                cartCheckout.CardExpiration,
                cartCheckout.CardSecurityCode,
                eventRequestId,
                cart);

            await _eventBus.PublishAsync(eventMessage);

            return Accepted();
        }
    }
}