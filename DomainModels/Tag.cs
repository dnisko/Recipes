namespace DomainModels
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
