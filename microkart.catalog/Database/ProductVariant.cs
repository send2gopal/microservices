using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;

namespace microkart.catalog.Database
{
    public class ProductVariant : BaseEntity
    {
        public ProductVariant()
        {
            CreatedBy = "gthakur";
            UpdatedBy = "gthakur";
            CraetedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
        }
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
