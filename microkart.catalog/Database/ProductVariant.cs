using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microkart.catalog.Database
{
    public class ProductVariant
    {
        public ProductVariant()
        {
            //CreatedBy = "gthakur";
            //UpdatedBy = "gthakur";
            //CraetedDate = DateTime.Now;
            //UpdatedDate = DateTime.Now;
        }

        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string variant_id { get; set; }

        [MaxLength(50)]
        public string sku { get; set; }
        [MaxLength(10)]
        public string size { get; set; }
        [MaxLength(50)]
        public string color { get; set; }

        //Navigation properties
        public virtual List<ProductImages> productImages { get; set; }
        public virtual Product product { get; set; }
    }
}
