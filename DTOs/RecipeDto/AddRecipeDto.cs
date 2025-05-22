namespace DTOs.RecipeDto
{
    public class AddRecipeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string ImagePath { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int Servings { get; set; }
        public int Difficulty { get; set; }
        public int CategoryId { get; set; }
        public List<IngredientDto.IngredientDto> Ingredients { get; set; }
        public List<TagDto.TagDto> Tags { get; set; }
    }
}
