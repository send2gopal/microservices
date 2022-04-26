using microkart.shared.Abstraction;

namespace microkart.shared.Events
{
    public record OrderSubmittedPubSubEvent(
    string UserId,
    string UserEmail,
    string City,
    string Street,
    string State,
    string ZipCode,
    string AptOrUnit,
    string Country,
    string CardNumber,
    string CardHolderName,
    DateTime CardExpiration,
    string CardSecurityNumber,
    Guid RequestId,
    CartRequest Cart)
    : PubSubEvent;
}
