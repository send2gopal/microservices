namespace microkart.basket.Models
{
    public class CartRequest1
    {
        public bool IsActive { get; set; }

// public virtual List<CartItemRequest> Items { get; set; } = new List<CartItemRequest>();
    }

    public class CartItemRequest1
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = "";

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string ProductImageUrl { get; set; } = "";
        public decimal Totalprice => UnitPrice * Quantity;
    }
}
