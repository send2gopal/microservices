namespace microkart.catalog.Models
{
    public class ImageModel
    {
        public int image_id { get; set; }
        public string alt { get; set; }
        public string src { get; set; }
        public IFormFile Image { get; set; }

        public int productId { get; set; }
    }
}
