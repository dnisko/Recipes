using Common.Responses;
using DTOs.TagDto;

namespace Services.Interfaces
{
    public interface ITagService
    {
        Task<CustomResponse<List<TagDto>>> GetPopularTagsAsync();
        Task<CustomResponse<List<TagDto>>> GetTagsByARecipeAsync(int id);
    }
}
