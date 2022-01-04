using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonarchX.Data.Models
{
    public class Sale
    {

        //Invoice ID
        [Key]
        public int Id { get; set; }
        public string InvoiceId { get; set; }
        public decimal TotalSale { get; set; }
        //Items Sold
        public List<Product> Items { get; set; }
        [StringLength(50)]
        public string Notes { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}