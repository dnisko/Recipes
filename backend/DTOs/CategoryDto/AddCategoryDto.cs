using System.ComponentModel.DataAnnotations;

namespace DTOs.CategoryDto
{
    public class AddCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
