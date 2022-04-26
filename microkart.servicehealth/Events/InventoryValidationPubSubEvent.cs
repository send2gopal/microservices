using microkart.shared.Abstraction;

namespace microkart.shared.Events
{
    public record InventoryValidationPubSubEvent(
    int OrderId,
    List<InventoryItems> InventoryItems,
    Guid CorrelationId
    )
    : PubSubEvent;

    public record InventoryItems(
    int ProductId,
    int Quantity
    );
}
