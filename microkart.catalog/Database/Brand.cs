using microkart.shared.Database;

namespace microkart.catalog.Database
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string Logo { get; set; }

        public string Description { get; set; }
        public string BannerImage { get; set; }
    }
}