namespace microkart.catalog.Database
{
    public class ProductImage
    {
    }
    public class ProductImage
    {
        public int image_id { get; set; }
        public string alt { get; set; }
        public string src { get; set; }
        public virtual List<int> Variant_id { get; set; }
    }
}
