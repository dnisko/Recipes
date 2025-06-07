using AutoMapper;
using Common.Exceptions.CategoryException;
using Common.Exceptions.RecipeException;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.CategoryDto;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System.Collections.Generic;

namespace Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        
        private readonly ILogger<ICategoryService> _logger;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,
            
            ILogger<ICategoryService> logger,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CustomResponse<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            try
            {
                //var categories = await _categoryRepository.GetAllAsync();
                //if (categories == null || !categories.Any())
                //{
                //    _logger.LogError("No categories found.");
                //    return new CustomResponse<List<CategoryDto>>($"No categories found.");
                //}
                //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
                //return new CustomResponse<List<CategoryDto>>(categoriesDto);
                var categories = await _categoryRepository.GetAllAsync();
                var response = CustomResponseFactory.FromList(categories, "No categories found.");

                if (!response.IsSuccessful)
                {
                    _logger.LogError(response.Errors.FirstOrDefault() ?? "No categories found.");
                }
                var categoriesDto = _mapper.Map<CustomResponse<List<CategoryDto>>>(response);
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
                //if (categoriesWithRecipes == null || !categoriesWithRecipes.Any())
                //{
                //    _logger.LogError("No categories with recipes found.");
                //    return new CustomResponse<List<CategoryDto>>($"No categories with recipes found.");
                //}
                var response = CustomResponseFactory.FromList(categoriesWithRecipes, "No categories with recipes found.");
                var categoriesWithRecipesDto = _mapper.Map<CustomResponse<List<CategoryDto>>>(response);
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
                    return CustomResponse.Fail($"Category with id {id} not found.");
                }

                //var categoryToDelete = _mapper.Map<Category>(category);
                await _categoryRepository.DeleteAsync(category);

                return CustomResponse.Success($"Deleted category with id: {id}.");
            }
            catch (CategoryNotFoundException ex)
            {
                throw new CategoryNotFoundException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<CategoryDto>> AddCategory(AddCategoryDto categoryDto)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(categoryDto);
                await _categoryRepository.AddAsync(categoryEntity);

                var categoryDtoResult = _mapper.Map<CategoryDto>(categoryEntity);
                //return new CustomResponse<CategoryDto>(categoryDtoResult);
                return CustomResponse<CategoryDto>.Success(categoryDtoResult);
            }
            catch (CategoryDataException ex)
            {
                throw new CategoryDataException($"Error while adding category: {ex.Message}");
            }
        }
    }
}
