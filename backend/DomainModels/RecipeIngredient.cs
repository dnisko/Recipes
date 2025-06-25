namespace DomainModels
{
    public class RecipeIngredient
    {
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        // Optional: quantity and unit here if needed
        public float Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
