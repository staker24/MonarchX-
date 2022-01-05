using System.ComponentModel.DataAnnotations;

namespace MonarchX.Models
{
    public class CustomerDto
    {    
        [Key]
        public int CustomerId { get; set; }
        public string StreetAddress { get; set; }
        public string Address2 { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }  
        public  decimal AmountOwed { get; set; }
    }
}