using System.ComponentModel.DataAnnotations;

namespace MonarchX.Models
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }
             
        [StringLength(30)]
        public string ItemName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    
    }
}