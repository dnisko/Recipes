using System.ComponentModel.DataAnnotations;

namespace DTOs.IngredientDto
{
    public class IngredientDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Range(0.01, 1000)]
        public float Quantity { get; set; }
        [Required]
        [MaxLength(20)]
        public string Unit { get; set; }
        public int RecipeId { get; set; }
    }
}
