using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonarchX.Data.Models
{
    public class Sale
    {

        //Invoice ID
        public int Id { get; set; }

        public Customer Customer { get; set; } 
        
        [ForeignKey("Customer")]
        public int CustomerFK { get; set; }
        public decimal TotalSale { get; set; }
        //Items Sold
        public List<Product> Items { get; set; }
        [StringLength(50)]
        public string Notes { get; set; }
    }
}