﻿using AutoMapper;
using Common.Exceptions.IngredientException;
using Common.Exceptions.RecipeException;
using Common.Exceptions.TagException;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs;
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
        public async Task<CustomResponse<PaginatedResult<RecipeDto>>> GetAllRecipesAsync(RecipePaginationParams paginationParams)
        {
            var paged = await _recipeRepository.GetAllRecipesAsync(paginationParams);

            if (!paged.Items.Any())
                return CustomResponse<PaginatedResult<RecipeDto>>.Fail("No recipes found.");

            var mapped = _mapper.Map<List<RecipeDto>>(paged.Items);
            var result = new PaginatedResult<RecipeDto>(mapped, paged.TotalRecords, paginationParams.PageNumber, paginationParams.PageSize);

            return CustomResponse<PaginatedResult<RecipeDto>>.Success(result);
        }

        public async Task<CustomResponse<RecipeDto>> GetRecipeByIdAsync(int id)
        {
            try
            {
                var recipe = await _recipeRepository.GetRecipeDetails(id);
                if (recipe == null)
                {
                    _logger.LogError($"Recipe with id {id} not found.");
                    return CustomResponse<RecipeDto>.Fail($"Recipe with id {id} not found.");
                }
                var recipeDto = _mapper.Map<RecipeDto>(recipe);
                return CustomResponse<RecipeDto>.Success(recipeDto);
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
                var response = CustomResponseFactory.FromList(recipes, $"No recipes found for category with id {categoryId}.");
                var recipesDto = _mapper.Map<CustomResponse<List<RecipeDto>>>(response);
                return recipesDto;
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
                //if (recipes == null || !recipes.Any())
                //{
                //    _logger.LogError($"No recipes found with keyword {keyword}.");
                //    return new CustomResponse<List<RecipeDto>>($"No recipes found with keyword {keyword}.");
                //}
                var response = CustomResponseFactory.FromList(recipes, $"No recipes found with keyword {keyword}.");
                var recipesDto = _mapper.Map<CustomResponse<List<RecipeDto>>>(response);
                return recipesDto;
            }
            catch (RecipeDataException ex)
            {
                throw new RecipeDataException($"Error while getting the categories: {ex.Message}");
            }
        }
        public async Task<CustomResponse> AddRecipeAsync(AddRecipeDto addRecipeDto)
        {
            try
            {
                // Map AddRecipeDto to Recipe domain entity, including join entities
                var recipeEntity = _mapper.Map<Recipe>(addRecipeDto);

                // Map Ingredients from DTO to RecipeIngredients join entities
                recipeEntity.RecipeIngredients = addRecipeDto.Ingredients.Select(i => new RecipeIngredient
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList();

                // Map Tags from DTO to RecipeTags join entities
                recipeEntity.RecipeTags = addRecipeDto.Tags.Select(t => new RecipeTag
                {
                    TagId = t.TagId
                }).ToList();

                await _recipeRepository.AddRecipeWithRelationsAsync(recipeEntity);

                var recipeDto = _mapper.Map<RecipeDto>(recipeEntity);
                return CustomResponse<RecipeDto>.Success(recipeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding recipe");
                return CustomResponse<RecipeDto>.Fail("An error occurred while adding the recipe.");
            }
        }
        public async Task<CustomResponse> UpdateRecipeAsync(UpdateRecipeDto updateRecipeDto)
        {
            try
            {
                // Map UpdateRecipeDto to Recipe domain entity, including join entities
                var recipeEntity = _mapper.Map<Recipe>(updateRecipeDto);

                // Map Ingredients from DTO to RecipeIngredients join entities
                recipeEntity.RecipeIngredients = updateRecipeDto.Ingredients.Select(i => new RecipeIngredient
                {
                    IngredientId = i.IngredientId,
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList();

                // Map Tags from DTO to RecipeTags join entities
                recipeEntity.RecipeTags = updateRecipeDto.Tags.Select(t => new RecipeTag
                {
                    TagId = t.TagId
                }).ToList();

                await _recipeRepository.UpdateRecipeWithRelationsAsync(recipeEntity);

                var recipeDto = _mapper.Map<RecipeDto>(recipeEntity);
                return CustomResponse<RecipeDto>.Success(recipeDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating recipe");
                return CustomResponse<RecipeDto>.Fail("An error occurred while updating the recipe.");
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
                    return CustomResponse.Fail($"Recipe with id {id} not found.");
                }
                await _recipeRepository.DeleteAsync(recipe);
                //await _recipeRepository.SaveChangesAsync();

                return CustomResponse.Success($"Deleted recipe with id {id}.");
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
                //if (ingredients == null || !ingredients.Any())
                //{
                //    _logger.LogError($"No ingredients found for recipe with id {recipeId}.");
                //    return new CustomResponse<List<IngredientDto>>($"No ingredients found for recipe with id {recipeId}.");
                //}
                var result = CustomResponseFactory.FromList(ingredients, $"No ingredients found for recipe with id {recipeId}.");
                var ingredientDto = _mapper.Map<CustomResponse<List<IngredientDto>>>(result);
                return ingredientDto;
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
                //if (tags == null || !tags.Any())
                //{
                //    _logger.LogError($"No tags found.");
                //    return new CustomResponse<List<TagDto>>($"No tags found.");
                //}
                var response = CustomResponseFactory.FromList(tags, "No tags found.");
                var tagDto = _mapper.Map<CustomResponse<List<TagDto>>>(response);
                return tagDto;
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
                //if (tags == null || !tags.Any())
                //{
                //    _logger.LogError($"No tags found.");
                //    return new CustomResponse<List<TagDto>>($"No tags found.");
                //}
                var result = CustomResponseFactory.FromList(tags, "No tags found.");
                var tagDto = _mapper.Map<CustomResponse<List<TagDto>>>(result);
                return tagDto;
            }
            catch (TagDataException ex)
            {
                throw new TagDataException($"Error while getting the categories: {ex.Message}");
            }
        }
    }
}
