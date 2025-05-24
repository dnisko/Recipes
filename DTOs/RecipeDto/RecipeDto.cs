using DomainModels;

namespace DTOs.RecipeDto
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Instructions { get; set; }
        public string? ImagePath { get; set; }
        public string? PrepTime { get; set; }
        public string? CookTime { get; set; }
        public double Servings { get; set; }
        public int Difficulty { get; set; }
        public int CategoryId { get; set; }

        // Change to mutable lists for AutoMapper compatibility
        public List<IngredientDto.IngredientDto> Ingredients { get; set; } = new();
        public List<TagDto.TagDto> Tags { get; set; } = new();
    }
}
