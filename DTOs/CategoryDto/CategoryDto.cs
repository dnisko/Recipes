namespace DTOs.CategoryDto
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public ICollection<RecipeDto.RecipeDto> Recipes { get; set; } = new List<RecipeDto.RecipeDto>();
    }
}
