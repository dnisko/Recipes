using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class Ingredient : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();
    }
}
