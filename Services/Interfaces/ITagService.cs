using DTOs.TagDto;

namespace Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagDto>> GetPopularTagsAsync();
        Task<TagDto> GetTagsByARecipeAsync(int id);
    }
}
