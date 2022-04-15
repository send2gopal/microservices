using microkart.shared.Database;

namespace microkart.catalog.Database
{
    public class Variant: BaseEntity
    {
        public int variant_id { get; set; }
        public string sku { get; set; }
        public string size { get; set; }
        public string color { get; set; }
    }
}
