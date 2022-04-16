using microkart.shared.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace microkart.catalog.Database
{
    public class ProductImages : BaseEntity
    {
        public int image_id { get; set; }
        public string alt { get; set; }
        public string src { get; set; }
        public virtual Product product{get;set;}

    }
}
