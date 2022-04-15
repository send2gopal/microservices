using System.ComponentModel.DataAnnotations.Schema;

namespace microkart.catalog.Models
{
    public class ProductViewModel
    {
        
        public string title { get; set; }

        public string description { get; set; }

       
        public decimal price { get; set; }

        
        public int discount { get; set; }

        
        public int stock { get; set; }

       
        public bool IsNew { get; set; }

        public bool sale { get; set; }

        public string type { get; set; }

        public int brandID { get; set; }

        public int categoryID { get; set; }
        public string tags { get; set; } = null!;
        public string collection { get; set; } = null!;
        public int imageId { get; set; }

        public DateTime createdDate { get; set; }=DateTime.Now;
        public string createdBy { get; set; } = "";
        public DateTime updatedDate { get; set; }=DateTime.Now;
        public string updatedBy { get; set; } = "";
        public string brandName { get; set; }   ="";
        public string categoryName { get; set; } ="";
        
        public string imagesrc { get; set; }="";





    }
}
