using System.ComponentModel.DataAnnotations;

namespace MonarchX.Data.Models
{
    public class Product
    {   
        [Key]
        public int Id { get; set; }
        public string ItemNumber { get; set; }
             
        [StringLength(30)]
        public string ItemName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}