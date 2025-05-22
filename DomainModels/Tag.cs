using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class Tag : BaseEntity
    {
        [MaxLength(20)]
        public string? Name { get; set; }
        //public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
