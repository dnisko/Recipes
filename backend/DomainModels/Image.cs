namespace DomainModels
{
    public class Image : BaseEntity
    {
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }

        public string? Url { get; set; }
        public string? Description { get; set; }
    }
}
 