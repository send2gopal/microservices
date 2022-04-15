using microkart.catalog.Database;
using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;

namespace microkart.catalog.Database
{
 
    public class Product : BaseEntity
    {
        [MaxLength(100)]
        [Required]
        public string Title { get; set; }
        [MaxLength(500)]
        [Required]
        public string description { get; set; }
        public string type { get; set; }

        public string collection { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public bool sale { get; set; }
        [Required]
        public string discount { get; set; }
        [Required]
        public int stock { get; set; }
        [Required]
        public bool IsNew { get; set; }
        public string tags { get; set; } = null!;

        //Navigation Properties
        public virtual Brand Brand { get; set; }
        public virtual ProductCatagory Category { get; set; }
        public virtual ProductImage images { get; set; } = null!;
    }
}