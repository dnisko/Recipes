﻿using DataAccess.Interfaces;
using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        private readonly RecipeDbContext _context;
        public IngredientRepository(RecipeDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByRecipe(int recipeId)
        {
            return await _context.RecipeIngredients
                .Where(ri => ri.RecipeId == recipeId)
                .Include(ri => ri.Ingredient)
                .Select(ri => ri.Ingredient)
                .ToListAsync();
        }
    }
}
