using microkart.shared.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace microkart.catalog.Database
{
    public class ProductImages : BaseEntity
    {
        public string AltText { get; set; }
        public string Source { get; set; }
        public virtual Product Product{get;set;}

    }
}
