using System.ComponentModel.DataAnnotations;

namespace MonarchX.Models
{
    public class ProductDto
    {   
        [Key]
        public string ItemNumber { get; set; }       
        public string ItemName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    
    }
}