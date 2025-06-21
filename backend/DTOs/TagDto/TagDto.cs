using System.ComponentModel.DataAnnotations;

namespace DTOs.TagDto
{
    public class TagDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
       // public ICollection<RecipeDto.RecipeDto> Recipes { get; set; } = new List<RecipeDto.RecipeDto>();
    }
}
