namespace DTOs.IngredientDto
{
    public class AddIngredientDto
    {
        public string? Name { get; set; }
        public float Quantity { get; set; }
        public string? Unit { get; set; }
        public int RecipeId { get; set; }
        // Additional properties can be added here if needed
        // For example, you might want to include a description or other metadata
    }
}
