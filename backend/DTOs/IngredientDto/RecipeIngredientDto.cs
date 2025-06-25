namespace DTOs.IngredientDto
{
    public class RecipeIngredientDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public float Quantity { get; set; }
        public string? Unit { get; set; }
    }
}
