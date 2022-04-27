namespace microkart.order.Models
{
    public class OrderItemResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ProductImageUrl { get; set; } = "";
    }
}