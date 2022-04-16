using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;

namespace microkart.catalog.Database
{
    public class ProductVariant:BaseEntity
    {
        public int variant_id { get; set; }
        [MaxLength(50)]
        public string sku { get; set; }
        [MaxLength(10)]
        public string size { get; set; }
        [MaxLength(50)]
        public string color { get; set; }

        //Navigation properties
        public virtual ProductImages productImages { get; set; }
        public virtual Product product { get; set; }
    }
}
