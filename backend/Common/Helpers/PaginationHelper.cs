using System.Reflection;
using Common.Responses;
using DomainModels.Enums;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace Common.Helpers
{
    public class PaginationHelper
    {
        public static async Task<PaginatedResult<T>> ApplyPaginationAsync<T>(
            IQueryable<T> query,
            PaginationParams baseParams)
        {

            if (baseParams.Id.HasValue && typeof(T).GetProperty("Id") != null)
            {
                query = query.Where(x => EF.Property<int>(x, "Id") == baseParams.Id.Value);
            }

            if (!string.IsNullOrWhiteSpace(baseParams.SearchKeyword) && typeof(T).GetProperty("Name") != null)
            {
                query = query.Where(x =>
                    EF.Functions.Like(EF.Property<string>(x, "Name"), $"%{baseParams.SearchKeyword}%"));
            }

            // Filtering for Recipe specific properties
            if (baseParams is RecipePaginationParams recipeParams)
            {
                

                if (recipeParams.CategoryId.HasValue && typeof(T).GetProperty("CategoryId") != null)
                {
                    query = query.Where(x =>
                        EF.Property<int>(x, "CategoryId") == recipeParams.CategoryId.Value);
                }

                if (recipeParams.Difficulty.HasValue && typeof(T).GetProperty("Difficulty") != null)
                {
                    query = query.Where(x =>
                        EF.Property<int>(x, "Difficulty") == recipeParams.Difficulty.Value);
                }
            }

            if (baseParams is CategoryPaginationParams categoryParams)
            {
                if (categoryParams.HasRecipe.HasValue && typeof(T).GetProperty("Recipes") != null)
                {
                    if (categoryParams.HasRecipe.Value)
                    {
                        query = query.Where(x =>
                            EF.Property<ICollection<object>>(x, "Recipes").Count > 0);
                    }
                    else
                    {
                        query = query.Where(x =>
                            EF.Property<ICollection<object>>(x, "Recipes").Count == 0);
                    }
                }
            }

            if (baseParams is UserPaginationParams userParams)
            {
                if (userParams.Id != null && typeof(T).GetProperty("Id") != null)
                {
                    query = query.Where(x =>
                        EF.Property<int>(x, "Id") == userParams.Id.Value);
                }
                if (userParams.Username != null && typeof(T).GetProperty("Username") != null)
                {
                    query = query.Where(x =>
                        EF.Property<string>(x, "Username").Contains(userParams.Username));
                }
                if (userParams.Email != null && typeof(T).GetProperty("Email") != null)
                {
                    query = query.Where(x =>
                        EF.Property<string>(x, "Email").Contains(userParams.Email));
                }
                if (userParams.Role != null && typeof(T).GetProperty("Role") != null)
                {
                    query = query.Where(x =>
                        EF.Property<string>(x, "Role").Equals(Enum.GetName(typeof(UserRole), 
                        userParams.Role), 
                        StringComparison.OrdinalIgnoreCase));
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(baseParams.SortBy))
            {
                var prop = typeof(T).GetProperty(baseParams.SortBy,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (prop != null)
                {
                    query = baseParams.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(x => EF.Property<object>(x, prop.Name))
                        : query.OrderBy(x => EF.Property<object>(x, prop.Name));
                }
            }

            // Pagination
            var totalRecords = await query.CountAsync();
            var items = await query
                .Skip((baseParams.PageNumber - 1) * baseParams.PageSize)
                .Take(baseParams.PageSize)
                .ToListAsync();

            return new PaginatedResult<T>(items, totalRecords, baseParams.PageNumber, baseParams.PageSize);
        }
    }
}
