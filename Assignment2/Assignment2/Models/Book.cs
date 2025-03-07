using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class Book
    {
        [Required(ErrorMessage = "{0} is required")]
        public int? Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Author { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int? YearPublished { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string Genre { get; set; }
    }
}
