using DTOs.IngredientDto;
using DTOs.TagDto;
using System.ComponentModel.DataAnnotations;

namespace DTOs.RecipeDto
{
    public class AddRecipeDto()
    {
        [Required] [MaxLength(100)] public string? Name { get; set; }

        public string? Description { get; set; }
        public string? Instructions { get; set; }
        public string? ImagePath { get; set; }
        public string? PrepTime { get; set; }
        public string? CookTime { get; set; }
        public double? Servings { get; set; }
        public int Difficulty { get; set; }
        public int CategoryId { get; set; }

        // Initialize collections directly
        public List<RecipeIngredientDto> Ingredients { get; set; } = new();
        public List<RecipeTagDto> Tags { get; set; } = new();
    }
}
