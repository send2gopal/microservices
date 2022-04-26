using microkart.shared.Database;

namespace microkart.basket.Database
{
    public class Cart : BaseEntity
    {
        public string UserId { get; set; } = "";
        public bool IsActive { get; set; }

        public virtual ICollection<CartItem> Items { get; set; } 
    }
}
