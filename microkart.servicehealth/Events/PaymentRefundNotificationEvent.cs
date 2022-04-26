using microkart.shared.Abstraction;

namespace microkart.shared.Events
{
    public record PaymentRefundNotificationEvent(
    Guid UserId,
    string OrderNumber,
    string UserEmail,
    string CardNumberEnding,
    decimal Amount,
    string RefundReferenceNumber,
    Guid CorrelationId)
    : PubSubEvent;
}
