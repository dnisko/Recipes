using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class Recipe : BaseEntity
    {
        //public List<Ingredient> _ingredients { get; set; } = [];
        //public List<Tag> _tags { get; set; } = [];

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Instructions { get; set; }
        public string? ImagePath { get; set; }
        public string? PrepTime { get; set; }
        public string? CookTime { get; set; }
        public double Servings { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        //[Required]
        //public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        ////public ICollection<Image> Images { get; set; } = new List<Image>();
        //public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
