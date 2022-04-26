using microkart.shared.Database;

namespace microkart.order.Database
{
    public class OrderEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public int OrderStatus { get; set; }
        public string Enail { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        public string PaymentReferenceNumber { get; set; } = string.Empty;
        public string RefundReferenceNumber { get; set; } = string.Empty;

        public virtual List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public virtual ShippingAddress ShippingAddress { get; set; } = new ShippingAddress();
        public virtual PaymentInformation PaymentInformation { get; set; } = new PaymentInformation();
    }
}
