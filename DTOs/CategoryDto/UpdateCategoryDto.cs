using System.ComponentModel.DataAnnotations;

namespace DTOs.CategoryDto
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
