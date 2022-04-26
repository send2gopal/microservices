namespace microkart.shared.Events
{
    public class CartRequest
    {
        public bool IsActive { get; set; }

        public virtual List<CartRequestItem> Items { get; set; } = new List<CartRequestItem>();
    }

    public class CartRequestItem
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = "";

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string ProductImageUrl { get; set; } = "";
        public decimal Totalprice => UnitPrice * Quantity;
    }
}
