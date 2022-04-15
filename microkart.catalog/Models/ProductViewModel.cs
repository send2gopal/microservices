namespace microkart.catalog.Models
{
    public class ProductViewModel
    {
        
        public string Name { get; set; }

        public string Description { get; set; }

       
        public decimal Price { get; set; }

        
        public int Discount { get; set; }

        
        public int QuantityInStock { get; set; }

       
        public bool IsDeleted { get; set; }

        public string Pitchures { get; set; }

        public int brandID { get; set; }

        public int categoryID { get; set; }


    }
}
