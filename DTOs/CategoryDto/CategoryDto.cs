using System.ComponentModel.DataAnnotations;

namespace DTOs.CategoryDto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Optional: Include this if you want to return recipe info with the category
        public List<RecipeDto.RecipeDto> Recipes { get; set; }
    }
}
