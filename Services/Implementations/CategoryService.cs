using AutoMapper;
using Common.Exceptions.CategoryException;
using Common.Exceptions.IngredientException;
using Common.Exceptions.TagException;
using Common.Responses;
using DataAccess.Implementations;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.CategoryDto;
using DTOs.IngredientDto;
using DTOs.TagDto;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ILogger<IRecipeService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,
            IIngredientRepository ingredientRepository,
            ITagRepository tagRepository,
            ILogger<IRecipeService> logger,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _ingredientRepository = ingredientRepository;
            _tagRepository = tagRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories == null || !categories.Any())
                {
                    _logger.LogError("No categories found.");
                    return new CustomResponse<List<CategoryDto>>($"No categories found.");
                }
                var categoriesDto = _mapper.Map<CustomResponse<List<CategoryDto>>>(categories);
                return categoriesDto;
            }
            catch (CategoryDataException ex)
            {
                throw new CategoryDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<CategoryDto>>> GetCategoriesWithTheirRecipesAsync()
        {
            try
            {
                var categoriesWithRecipes = await _categoryRepository.GetCategoriesWithRecipesAsync();
                if (categoriesWithRecipes == null || !categoriesWithRecipes.Any())
                {
                    _logger.LogError("No categories with recipes found.");
                    return new CustomResponse<List<CategoryDto>>($"No categories with recipes found.");
                }
                var categoriesWithRecipesDto = _mapper.Map<CustomResponse<List<CategoryDto>>>(categoriesWithRecipes);
                return categoriesWithRecipesDto;
            }
            catch (CategoryDataException ex)
            {
                throw new CategoryDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    _logger.LogError($"Category with id {id} not found.");
                    return new CustomResponse($"Category with id {id} not found.");
                }

                var categoryToDelete = _mapper.Map<Category>(category);
                await _categoryRepository.DeleteAsync(categoryToDelete);

                return new CustomResponse()
                {
                    IsSuccessful = true
                };
            }
            catch (CategoryNotFoundException ex)
            {
                throw new CategoryNotFoundException($"Error while getting the categories: {ex.Message}");
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
