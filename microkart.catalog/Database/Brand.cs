using microkart.shared.Database;
using System.ComponentModel.DataAnnotations;

namespace microkart.catalog.Database
{
    public class Brand : BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public string Logo { get; set; }
        [MaxLength(200)]
        [Required]
        public string Description { get; set; }
        public string BannerImage { get; set; }
    }
}