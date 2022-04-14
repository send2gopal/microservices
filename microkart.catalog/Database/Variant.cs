namespace microkart.catalog.Database
{
    public class Variant
    {
        public int variant_id { get; set; }
        public string sku { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public virtual int image_id { get; set; }
    }
}
