using microkart.shared.Abstraction;

namespace microkart.shared.Events
{
    public record ProcessPaymentPubSubEvent(
    Guid UserId,
    int OrderId,
    string CardNumber,
    string CardHolderName,
    DateTime CardExpiration,
    string CardSecurityNumber,
    decimal Amount,
    Guid CorrelationId)
    : PubSubEvent;

    public record ProcessRefundPubSubEvent(
    Guid UserId,
    int OrderId,
    string CardNumber,
    string CardHolderName,
    DateTime CardExpiration,
    string CardSecurityNumber,
    decimal Amount,
    Guid CorrelationId)
    : PubSubEvent;
}
