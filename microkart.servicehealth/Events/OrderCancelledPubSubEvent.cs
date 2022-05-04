using microkart.shared.Abstraction;

namespace microkart.shared.Events
{
    public record OrderCancelledPubSubEvent(
    Guid UserId,
    int OrderId,
    string UserEmail,
    string CardNumber,
    string CardHolderName,
    string CardExpiration,
    string CardSecurityNumber,
    decimal Amount,
    string PaymentReferenceNumber,
    Guid CorrelationId,
    CartRequest Cart)
    : PubSubEvent;
}
