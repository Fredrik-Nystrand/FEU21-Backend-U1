using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace backend.Models.Customer
{
    public class CustomerRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
