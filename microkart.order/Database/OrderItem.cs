using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microkart.order.Database
{
    public class OrderItem : BaseEntity
    {

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = "";

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string ProductImageUrl { get; set; } = "";

        public virtual OrderEntity Order { get; set; } = new OrderEntity();
    }
}
