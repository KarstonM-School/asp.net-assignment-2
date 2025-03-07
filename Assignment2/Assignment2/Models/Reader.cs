using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class Reader
    {
        [Required(ErrorMessage = "{0} is required")]
        public int? Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} must be a valid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "{0} is required")]       
        public string Address { get; set; } 
    }
}
