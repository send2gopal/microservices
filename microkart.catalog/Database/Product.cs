using microkart.catalog.Database;
using microkart.shared.Database;
namespace microkart.catalog.Database
{
   

    

    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string description { get; set; }
        public string type { get; set; }

        public List<string> collection { get; set; }

        public int price { get; set; }
        public bool sale { get; set; }
        public string discount { get; set; }
        public int stock { get; set; }
        public bool IsNew { get; set; }

        //Navigation Properties
        public virtual Brand Brand { get; set; }
        public virtual ProductCatagory Category { get; set; }
        public virtual List<string> tags { get; set; } = null!;
        public virtual List<Variant> variants { get; set; } = null!;
        public virtual List<ProductImage> images { get; set; } = null!;
    }
}