using microkart.basket.Database;
using microkart.shared.Abstraction;

namespace microkart.basket
{
    public record CheckoutPubSubEvent(
    string UserId,
    string UserEmail,
    string City,
    string Street,
    string State,
    string Country,
    string CardNumber,
    string CardHolderName,
    DateTime CardExpiration,
    string CardSecurityNumber,
    Guid RequestId,
    Cart Cart)
    : PubSubEvent;
}
