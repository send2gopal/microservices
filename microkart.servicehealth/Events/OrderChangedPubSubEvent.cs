using microkart.shared.Abstraction;

namespace microkart.shared.Events
{
    public record OrderChngedPubSubEvent(
    int OrderId,
    int Status,
    List<string> errors,
    Guid CorrelationId)
    : PubSubEvent;

    public record InventoryVaidationResultPubSubEvent(
    int OrderId,
    int Status,
    List<InventoryItems> updatedInventoryItems,
    List<string> errors,
    Guid CorrelationId)
    : PubSubEvent;
}
