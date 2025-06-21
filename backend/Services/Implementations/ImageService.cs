using AutoMapper;
using Common.Exceptions.ImageException;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.ImageDto;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly ILogger<IImageService> _logger;
        private readonly IMapper _mapper;
        public ImageService(IImageRepository imageRepository, ILogger<IImageService> logger, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CustomResponse<List<ImageDto>>> GetImagesByRecipeAsync(int recipeId)
        {
            try
            {
                var image = await _imageRepository.GetImagesByRecipe(recipeId);
                if (image == null || !image.Any())
                {
                    _logger.LogError($"No images found for recipe with id {recipeId}.");
                    return new CustomResponse<List<ImageDto>>($"No images found for recipe with id {recipeId}.");
                }
                var imageDto = _mapper.Map<List<ImageDto>>(image);
                return new CustomResponse<List<ImageDto>>(imageDto);
            }
            catch (ImageDataException ex)
            {
                throw new ImageDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<ImageDto>>> AddImageToRecipeAsync(int recipeId, ImageDto imageEntity)
        {
            try
            {
                //var image = _mapper.Map<ImageDto>(imageEntity);
                var createImageDto = new ImageDto
                {
                    RecipeId = recipeId,
                    Url = imageEntity.Url,
                    Description = imageEntity.Description
                };
                var imageEntityToAdd = _mapper.Map<Image>(createImageDto);
                if (imageEntityToAdd == null)
                {
                    _logger.LogError($"No image found for recipe with id {recipeId}.");
                    return new CustomResponse<List<ImageDto>>($"No image found for recipe with id {recipeId}.");
                }
                await _imageRepository.AddAsync(imageEntityToAdd);
                var imageDto = _mapper.Map<List<ImageDto>>(imageEntityToAdd);
                return new CustomResponse<List<ImageDto>>(imageDto);


                //var image = await _imageRepository.AddAsync(recipeId);
            }
            catch (ImageDataException ex)
            {
                throw new ImageDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<ImageDto>>> UpdateImageToRecipeAsync(int imageId)
        {
            try
            {
                var image = await _imageRepository.GetByIdAsync(imageId);
                if (image == null)
                {
                    _logger.LogError($"No image found with id {imageId}.");
                    return new CustomResponse<List<ImageDto>>($"No image found with id {imageId}.");
                }
                var imageDto = _mapper.Map<List<ImageDto>>(image);
                return new CustomResponse<List<ImageDto>>(imageDto);

            }
            catch (ImageDataException ex)
            {
                throw new ImageDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse> DeleteImageToRecipeAsync(int imageId)
        {
            try
            {
                var image = await _imageRepository.GetByIdAsync(imageId);
                if (image == null)
                {
                    _logger.LogError($"No image found with id {imageId}.");
                    return new CustomResponse<List<ImageDto>>($"No image found with id {imageId}.");
                }
                await _imageRepository.DeleteAsync(image);

                return new CustomResponse()
                {
                    IsSuccessful = true
                };
            }
            catch (ImageDataException ex)
            {
                throw new ImageDataException($"Error while getting the categories: {ex.Message}");
            }
        }
    }
}
