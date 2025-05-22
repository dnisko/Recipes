using System.ComponentModel.DataAnnotations;

namespace DTOs.IngredientDto
{
    public class IngredientDto
    {
        [Required]
        public string? Name { get; set; }
        public double Quantity { get; set; }
        public string? Unit { get; set; }
        //public int RecipeId { get; set; }
    }
}
