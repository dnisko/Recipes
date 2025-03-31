namespace DomainModels
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int Servings { get; set; }
        public DifficultyLevel Difficulty { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

    }
}
