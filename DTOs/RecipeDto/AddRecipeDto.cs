using System.ComponentModel.DataAnnotations;

namespace DTOs.RecipeDto
{
    public class AddRecipeDto
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Instructions { get; set; }
        public string? ImagePath { get; set; }
        public string? PrepTime { get; set; }
        public string? CookTime { get; set; }
        public double? Servings { get; set; }
        public int Difficulty { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public List<IngredientDto.IngredientDto>? Ingredients { get; set; }
        public List<TagDto.TagDto>? Tags { get; set; }
    }
}
