using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
