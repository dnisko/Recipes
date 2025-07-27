using System.Reflection;
using Common.Responses;
using DomainModels.Enums;
using DTOs.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Common.Helpers
{
    public class PaginationHelper
    {
        public static async Task<PaginatedResult<T>> ApplyPaginationAsync<T>(
            IQueryable<T> query,
            IPaginationParams baseParams)
        {
            var entityType = typeof(T);

            // Universal filtering by Id
            if (baseParams.Id.HasValue && entityType.GetProperty("Id") != null)
            {
                query = query.Where(x => EF.Property<int>(x, "Id") == baseParams.Id.Value);
            }

            // Universal search by Name
            if (!string.IsNullOrWhiteSpace(baseParams.SearchKeyword) && entityType.GetProperty("Name") != null)
            {
                query = query.Where(x =>
                    EF.Functions.Like(EF.Property<string>(x, "Name"), $"%{baseParams.SearchKeyword}%"));
            }

            // Recipe-specific filters
            if (baseParams is RecipePaginationParams recipeParams)
            {
                if (recipeParams.CategoryId.HasValue && entityType.GetProperty("CategoryId") != null)
                {
                    query = query.Where(x =>
                        EF.Property<int>(x, "CategoryId") == recipeParams.CategoryId.Value);
                }

                if (recipeParams.Difficulty.HasValue && entityType.GetProperty("Difficulty") != null)
                {
                    query = query.Where(x =>
                        EF.Property<int>(x, "Difficulty") == recipeParams.Difficulty.Value);
                }
            }


            // Category-specific filters
            if (baseParams is CategoryPaginationParams categoryParams &&
                categoryParams.HasRecipe.HasValue &&
                entityType.GetProperty("Recipes") != null)
            {
                query = categoryParams.HasRecipe.Value
                    ? query.Where(x => EF.Property<ICollection<object>>(x, "Recipes").Count > 0)
                    : query.Where(x => EF.Property<ICollection<object>>(x, "Recipes").Count == 0);
            }


            // User-specific filters
            if (baseParams is UserPaginationParams userParams)
            {
                if (!string.IsNullOrWhiteSpace(userParams.Username) && entityType.GetProperty("Username") != null)
                {
                    query = query.Where(x =>
                        EF.Property<string>(x, "Username").Contains(userParams.Username));
                }

                if (!string.IsNullOrWhiteSpace(userParams.Email) && entityType.GetProperty("Email") != null)
                {
                    query = query.Where(x =>
                        EF.Property<string>(x, "Email").Contains(userParams.Email));
                }

                if (userParams.Role.HasValue && entityType.GetProperty("Role") != null)
                {
                    var roleName = Enum.GetName(typeof(UserRole), userParams.Role);
                    if (!string.IsNullOrWhiteSpace(roleName))
                    {
                        query = query.Where(x =>
                            EF.Property<string>(x, "Role").Equals(roleName, StringComparison.OrdinalIgnoreCase));
                    }
                }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(baseParams.SortBy))
            {
                var sortProperty = entityType.GetProperty(baseParams.SortBy,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (sortProperty != null)
                {
                    query = baseParams.SortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(x => EF.Property<object>(x, sortProperty.Name))
                        : query.OrderBy(x => EF.Property<object>(x, sortProperty.Name));
                }
            }
            else
            {
                // Apply default sort by Id
                var defaultSort = entityType.GetProperty("Id"); // or "Username" or any field you like
                if (defaultSort != null)
                {
                    query = query.OrderBy(x => EF.Property<object>(x, defaultSort.Name));
                }
            }

            // Count and paginate
            var totalRecords = await query.CountAsync();
            var items = await query
                .Skip((baseParams.PageNumber - 1) * baseParams.PageSize)
                .Take(baseParams.PageSize)
                .ToListAsync();

            return new PaginatedResult<T>(items, totalRecords, baseParams.PageNumber, baseParams.PageSize);
        

        //// Pagination
        //var totalRecords = await query.CountAsync();
        //    var items = await query
        //        .Skip((baseParams.PageNumber - 1) * baseParams.PageSize)
        //        .Take(baseParams.PageSize)
        //        .ToListAsync();

        //    return new PaginatedResult<T>(items, totalRecords, baseParams.PageNumber, baseParams.PageSize);
        }
    }
}
