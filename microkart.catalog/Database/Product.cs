using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;

namespace microkart.catalog.Database
{
    public class Product : BaseEntity
    {

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Discount { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public string Pitchures { get; set; }

        public virtual ProductCatagory ProductCatagory { get; set; } = null!;

        public virtual Brand Brand { get; private set; } = null!;
    }
}