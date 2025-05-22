using DomainModels;

namespace DTOs.RecipeDto
{
    public class RecipeDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Instructions { get; set; }
        public string? ImagePath { get; set; }
        public string? PrepTime { get; set; }
        public string? CookTime { get; set; }
        public double Servings { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        public int CategoryId { get; set; }

        public ICollection<IngredientDto.IngredientDto> Ingredients { get; set; } = new List<IngredientDto.IngredientDto>();
        //public ICollection<ImageDto.ImageDto> Images { get; set; } = new List<ImageDto.ImageDto>();
        public ICollection<TagDto.TagDto> Tags { get; set; } = new List<TagDto.TagDto>();
    }
}
