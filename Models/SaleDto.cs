using System.ComponentModel.DataAnnotations;

namespace MonarchX.Models
{
    public class SaleDto
    {   
        //Invoice ID
        public int Id { get; set; }
        public CustomerDto Customer { get; set; }
        public decimal TotalSale { get; set; }
        //Items Sold
        public List<ProductDto> Items { get; set; }

        public string Notes { get; set; }

    }
}