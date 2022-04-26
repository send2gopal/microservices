using microkart.shared.Abstraction;

namespace microkart.shared.Events
{
    public record OrderConfirmationNotificationEvent(
    Guid UserId,
    int OrderId,
    string email,
    Guid CorrelationId)
    : PubSubEvent;
}
