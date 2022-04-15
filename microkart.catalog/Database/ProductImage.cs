using microkart.shared.Database;

namespace microkart.catalog.Database
{
    public class ProductImage: BaseEntity
    {
        public int image_id { get; set; }
        public string alt { get; set; }
        public string src { get; set; }
    }
}
