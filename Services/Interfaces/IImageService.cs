using DTOs.ImageDto;

namespace Services.Interfaces
{
    public interface IImageService
    {
        Task<List<ImageDto>> GetImagesByRecipeAsync(int recipeId);
    }
}
