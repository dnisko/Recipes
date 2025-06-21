using Common.Responses;
using DTOs.ImageDto;

namespace Services.Interfaces
{
    public interface IImageService
    {
        Task<CustomResponse<List<ImageDto>>> GetImagesByRecipeAsync(int recipeId);
        Task<CustomResponse<List<ImageDto>>> AddImageToRecipeAsync(int recipeId, ImageDto imageEntity);
        Task<CustomResponse<List<ImageDto>>> UpdateImageToRecipeAsync(int imageId);
        Task<CustomResponse> DeleteImageToRecipeAsync(int imageId);
    }
}
