using AutoMapper;
using Common.Exceptions.IngredientException;
using Common.Responses;
using DataAccess.Interfaces;
using DTOs.IngredientDto;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementations
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly ILogger<IIngredientService> _logger;
        private readonly IMapper _mapper;
        public IngredientService(IIngredientRepository ingredientRepository, ILogger<IIngredientService> logger, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _logger = logger;
            _mapper = mapper;
        }
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
    }
}
