namespace microkart.order.Models
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.New;
        public string Enail { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string PaymentReferenceNumber { get; set; } = string.Empty;
        public string RefundReferenceNumber { get; set; } = string.Empty;
        public virtual List<OrderItemResponse> Items { get; set; } = new List<OrderItemResponse>();
        public virtual ShippingAddressResponse ShippingAddress { get; set; } = new ShippingAddressResponse();
        public virtual PaymentInformationResponse PaymentInformation { get; set; } = new PaymentInformationResponse();
    }
}