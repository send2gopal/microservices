using microkart.shared.Database;

namespace microkart.basket.Database
{
    public class Cart : BaseEntity
    {
        public Cart()
        {

        }

        public Cart(string customerId)
        {
            UserId = customerId;
        }

        public string UserId { get; set; } = "";
        public bool IsActive { get; set; }

        public virtual List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
