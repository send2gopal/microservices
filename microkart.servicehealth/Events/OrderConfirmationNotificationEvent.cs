using microkart.shared.Abstraction;

namespace microkart.shared.Events
{
    public record OrderConfirmationNotificationEvent(
    Guid UserId,
    int OrderId,
    string email,
    Guid CorrelationId)
    : PubSubEvent;
    
    public record OrderShippedNotificationEvent(
    Guid UserId,
    int OrderId,
    string email,
    Guid CorrelationId)
    : PubSubEvent;

    public record OrderDeliverNotificationEvent(
    Guid UserId,
    int OrderId,
    string email,
    Guid CorrelationId)
    : PubSubEvent;
}
