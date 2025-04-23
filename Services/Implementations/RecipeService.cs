using AutoMapper;
using Common.Exceptions.RecipeException;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.RecipeDto;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ILogger<IRecipeService> _logger;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository, 
            ILogger<IRecipeService> logger, 
            IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CustomResponse<List<RecipeDto>>> GetAllRecipesAsync()
        {
            try
            {
                var recipes = await _recipeRepository.GetAllAsync();
                if (recipes == null || !recipes.Any())
                {
                    _logger.LogError("No recipes found.");
                    return new CustomResponse<List<RecipeDto>>($"No recipes found.");
                }
                var recipesDto = _mapper.Map<List<RecipeDto>>(recipes);
                return new CustomResponse<List<RecipeDto>>(recipesDto);
            }
            catch (RecipeDataException ex)
            {
                throw new RecipeDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<RecipeDto>>> GetRecipeByIdAsync(int id)
        {
            try
            {
                var recipe = await _recipeRepository.GetByIdAsync(id);
                if (recipe == null)
                {
                    _logger.LogError($"Recipe with id {id} not found.");
                    return new CustomResponse<List<RecipeDto>>($"Recipe with id {id} not found.");
                }
                var recipeDto = _mapper.Map<List<RecipeDto>>(recipe);
                return new CustomResponse<List<RecipeDto>>(recipeDto);
            }
            catch (RecipeDataException ex)
            {
                throw new RecipeDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<RecipeDto>>> GetRecipesByCategoryAsync(int categoryId)
        {
            try
            {
                var recipes = await _recipeRepository.GetRecipesByCategory(categoryId);
                if (recipes == null || !recipes.Any())
                {
                    _logger.LogError($"No recipes found for category with id {categoryId}.");
                    return new CustomResponse<List<RecipeDto>>($"No recipes found for category with id {categoryId}.");
                }
                var recipesDto = _mapper.Map<List<RecipeDto>>(recipes);
                return new CustomResponse<List<RecipeDto>>(recipesDto);
            }
            catch (RecipeDataException ex)
            {
                throw new RecipeDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<RecipeDto>>> SearchRecipesAsync(string keyword)
        {
            try
            {
                var recipes = await _recipeRepository.SearchRecipes(keyword);
                if (recipes == null || !recipes.Any())
                {
                    _logger.LogError($"No recipes found with keyword {keyword}.");
                    return new CustomResponse<List<RecipeDto>>($"No recipes found with keyword {keyword}.");
                }
                var recipesDto = _mapper.Map<List<RecipeDto>>(recipes);
                return new CustomResponse<List<RecipeDto>>(recipesDto);
            }
            catch (RecipeDataException ex)
            {
                throw new RecipeDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<RecipeDto>>> GetRecipesWithTagsAsync()
        {
            try
            {
                var recipes = await _recipeRepository.GetRecipesWithTags();
                if (recipes == null || !recipes.Any())
                {
                    _logger.LogError($"No recipes found with tags.");
                    return new CustomResponse<List<RecipeDto>>($"No recipes found with tags.");
                }
                var recipesDto = _mapper.Map<List<RecipeDto>>(recipes);
                return new CustomResponse<List<RecipeDto>>(recipesDto);
            }
            catch (RecipeDataException ex)
            {
                throw new RecipeDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<RecipeDto>>> GetRecipeDetailsAsync(int recipeId)
        {
            try
            {
                var recipe = await _recipeRepository.GetRecipeDetails(recipeId);
                if (recipe == null)
                {
                    _logger.LogError($"Recipe with id {recipeId} not found.");
                    return new CustomResponse<List<RecipeDto>>($"Recipe with id {recipeId} not found.");
                }
                var recipeDto = _mapper.Map<List<RecipeDto>>(recipe);
                return new CustomResponse<List<RecipeDto>>(recipeDto);
            }
            catch (RecipeDataException ex)
            {
                throw new RecipeDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse> AddRecipeAsync(RecipeDto recipe)
        {
            try
            {
                var recipeEntity = _mapper.Map<Recipe>(recipe);
                await _recipeRepository.AddAsync(recipeEntity);
                //await _recipeRepository.SaveChangesAsync();
                var recipeDto = _mapper.Map<RecipeDto>(recipeEntity);
                return new CustomResponse<RecipeDto>(recipeDto);
            }
            catch (RecipeDataException ex)
            {
                throw new RecipeDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse> UpdateRecipeAsync(RecipeDto recipe)
        {
            try
            {
                var recipeEntity = _mapper.Map<Recipe>(recipe);
                await _recipeRepository.UpdateAsync(recipeEntity);
                //await _recipeRepository.SaveChangesAsync();
                var recipeDto = _mapper.Map<RecipeDto>(recipeEntity);
                return new CustomResponse<RecipeDto>(recipeDto);
            }
            catch (RecipeDataException ex)
            {
                throw new RecipeDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse> DeleteRecipeAsync(int id)
        {
            try
            {
                var recipe = await _recipeRepository.GetByIdAsync(id);
                if (recipe == null)
                {
                    _logger.LogError($"Recipe with id {id} not found.");
                    return new CustomResponse($"Recipe with id {id} not found.");
                }
                var recipeToDelete = _mapper.Map<Recipe>(recipe);
                await _recipeRepository.DeleteAsync(recipeToDelete);
                //await _recipeRepository.SaveChangesAsync();

                return new CustomResponse()
                {
                    IsSuccessful = true
                };
            }
            catch (RecipeNotFoundException ex)
            {
                throw new RecipeNotFoundException($"Error while getting the categories: {ex.Message}");
            }
        }
    }
}
