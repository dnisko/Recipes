using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class Tag : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;
        public ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();
    }
}
