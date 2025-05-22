using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class Ingredient : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public double Quantity { get; set; }
        [MaxLength(10)]
        public string? Unit { get; set; }

        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
