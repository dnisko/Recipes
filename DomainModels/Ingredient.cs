using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class Ingredient : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        [Range(0.01, 1000)]
        public float Quantity { get; set; }
        [MaxLength(10)]
        public string? Unit { get; set; }

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
