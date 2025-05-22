using AutoMapper;
using Common.Exceptions.IngredientException;
using Common.Exceptions.RecipeException;
using Common.Exceptions.TagException;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.IngredientDto;
using DTOs.RecipeDto;
using DTOs.TagDto;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ILogger<IRecipeService> _logger;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository recipeRepository,
            IIngredientRepository ingredientRepository,
            ITagRepository tagRepository,
            ILogger<IRecipeService> logger, 
            IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _tagRepository = tagRepository;
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

        
        //Ingredients
        public async Task<CustomResponse<List<IngredientDto>>> GetIngredientsByRecipe(int recipeId)
        {
            try
            {
                var ingredients = await _ingredientRepository.GetIngredientsByRecipe(recipeId);
                if (ingredients == null || !ingredients.Any())
                {
                    _logger.LogError($"No ingredients found for recipe with id {recipeId}.");
                    return new CustomResponse<List<IngredientDto>>($"No ingredients found for recipe with id {recipeId}.");
                }
                var ingredientDto = _mapper.Map<List<IngredientDto>>(ingredients);
                return new CustomResponse<List<IngredientDto>>(ingredientDto);
            }
            catch (IngredientDataException ex)
            {
                throw new IngredientDataException($"Error while getting the categories: {ex.Message}");
            }
        }


        //Tag
        public async Task<CustomResponse<List<TagDto>>> GetPopularTagsAsync()
        {
            try
            {
                var tags = await _tagRepository.GetPopularTags();
                if (tags == null || !tags.Any())
                {
                    _logger.LogError($"No tags found.");
                    return new CustomResponse<List<TagDto>>($"No tags found.");
                }
                var tagDto = _mapper.Map<List<TagDto>>(tags);
                return new CustomResponse<List<TagDto>>(tagDto);
            }
            catch (TagDataException ex)
            {
                throw new TagDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<TagDto>>> GetTagsByARecipeAsync(int id)
        {
            try
            {
                var tags = await _tagRepository.GetTagsByRecipe(id);
                if (tags == null || !tags.Any())
                {
                    _logger.LogError($"No tags found.");
                    return new CustomResponse<List<TagDto>>($"No tags found.");
                }
                var tagDto = _mapper.Map<List<TagDto>>(tags);
                return new CustomResponse<List<TagDto>>(tagDto);
            }
            catch (TagDataException ex)
            {
                throw new TagDataException($"Error while getting the categories: {ex.Message}");
            }
        }
    }
}
