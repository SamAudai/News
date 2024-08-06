using System.ComponentModel.DataAnnotations;

namespace News.Shared.Models
{
    public class Category
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [MinLength(5, ErrorMessage = "Category name at least 5 chars required")]
        public string name { get; set; }
    }
}
