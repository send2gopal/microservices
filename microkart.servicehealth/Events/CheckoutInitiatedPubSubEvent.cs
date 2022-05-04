using microkart.shared.Abstraction;

namespace microkart.shared.Events
{
    public record CheckoutInitiatedPubSubEvent(
    Guid UserId,
    string UserEmail,
    string City,
    string Street,
    string State,
    string ZipCode,
    string AptOrUnit,
    string Country,
    string CardNumber,
    string CardHolderName,
    string CardExpiration,
    string CardSecurityNumber,
    Guid CorrelationId,
    CartRequest Cart)
    : PubSubEvent;
}
