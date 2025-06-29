using AutoMapper;
using Common.Exceptions.CategoryException;
using Common.Responses;
using DataAccess.Interfaces;
using DomainModels;
using DTOs;
using DTOs.CategoryDto;
using DTOs.RecipeDto;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

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

        public async Task<CustomResponse<PaginatedResult<CategorySimpleDto>>> GetAllCategoriesAsync(PaginationParams paginationParams)
        {
            try
            {
                var categories = await _categoryRepository.GetPagedAsync(paginationParams);

                if (!categories.Items.Any())
                {
                    return CustomResponse<PaginatedResult<CategorySimpleDto>>.Fail("No categories found.");
                }

                var mapped = _mapper.Map<List<CategorySimpleDto>>(categories.Items);
                var result = new PaginatedResult<CategorySimpleDto>(mapped, categories.TotalRecords,
                    paginationParams.PageNumber, paginationParams.PageSize);

                return CustomResponse<PaginatedResult<CategorySimpleDto>>.Success(result);
            }
            catch (CategoryDataException ex)
            {
                throw new CategoryDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<PaginatedResult<CategoryDto>>> GetCategoriesWithTheirRecipesAsync(PaginationParams paginationParams)
        {
            try
            {
                var categories = await _categoryRepository.GetCategoriesWithRecipesAsync(paginationParams);

                if (!categories.Items.Any())
                {
                    return CustomResponse<PaginatedResult<CategoryDto>>.Fail("No categories found.");
                }

                var mapped = _mapper.Map<List<CategoryDto>>(categories.Items);
                var result = new PaginatedResult<CategoryDto>(mapped, categories.TotalRecords,
                    paginationParams.PageNumber, paginationParams.PageSize);

                return CustomResponse<PaginatedResult<CategoryDto>>.Success(result);
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
