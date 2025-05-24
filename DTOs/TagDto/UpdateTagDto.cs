using System.ComponentModel.DataAnnotations;

namespace DTOs.TagDto
{
    public class UpdateTagDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }
        // Additional properties can be added here if needed
        // For example, you might want to include a description or other metadata
    }
}
