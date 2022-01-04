using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonarchX.Data.Models
{
    public class Customer
    {   
        [Key]
         public string CustomerId { get; set; }

         [StringLength(50)]
        public string StreetAddress { get; set; }

        [StringLength(30)]
        public string Address2 { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(12)]
        public string FirstName { get; set; }

        [StringLength(12)]
        public string LastName { get; set; }

        [StringLength(15)]
        public string Email { get; set; }
        public PhoneAttribute PhoneNumber { get; set; }
        public  decimal AmountOwed { get; set; }
    }
}