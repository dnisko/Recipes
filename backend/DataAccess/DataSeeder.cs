using DomainModels;

namespace DataAccess
{
    public static class DataSeeder
    {
        public static void SeedData(RecipeDbContext context)
        {
            if (context.Categories.Any()) return;
            // Seed Categories
            var categories = new List<Category>
            {
                new Category { Name = "Breakfast" },
                new Category { Name = "Lunch" },
                new Category { Name = "Dinner" }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Seed Ingredients
            var ingredients = new List<Ingredient>
            {
                new() { Name = "Eggs" },
                new() { Name = "Flour" },
                new() { Name = "Milk" },
                new() { Name = "Tomato" },
                new() { Name = "Pasta" }
            };
            context.Ingredients.AddRange(ingredients);
            context.SaveChanges();

            // Seed Tags
            var tags = new List<Tag>
            {
                new() { Name = "Vegetarian" },
                new() { Name = "Quick" },
                new() { Name = "Spicy" }
            };
            context.Tags.AddRange(tags);
            context.SaveChanges();

            // Seed Recipes
            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Pancakes",
                    Description = "Fluffy pancakes with syrup",
                    Instructions = "Mix ingredients and cook on skillet.",
                    ImagePath = "images/pancakes.jpg",
                    PrepTime = "10 minutes",
                    CookTime = "15 minutes",
                    Servings = 4,
                    Difficulty = DifficultyLevel.Easy,
                    CategoryId = categories.First(c => c.Name == "Breakfast").Id,
                },
                new Recipe
                {
                    Name = "Spaghetti Bolognese",
                    Description = "Classic Italian pasta dish",
                    Instructions = "Cook pasta and prepare sauce with ground meat.",
                    ImagePath = "images/spaghetti.jpg",
                    PrepTime = "15 minutes",
                    CookTime = "30 minutes",
                    Servings = 2,
                    Difficulty = DifficultyLevel.Medium,
                    CategoryId = categories.First(c => c.Name == "Dinner").Id,
                },
                new Recipe
                {
                    Name = "Pasta Marinara",
                    Description = "Simple tomato pasta",
                    Instructions = "Boil pasta, cook sauce, mix together.",
                    ImagePath = "pasta.jpg",
                    PrepTime = "10min",
                    CookTime = "20min",
                    Servings = 2,
                    Difficulty = DifficultyLevel.Medium,
                    CategoryId = categories.First(c => c.Name == "Dinner").Id,
                }
            };
            context.Recipes.AddRange(recipes);
            context.SaveChanges();

            // Seed Ingredients
            var recipeIngredients = new List<RecipeIngredient>
            {
                // Pancakes ingredients
                new RecipeIngredient
                    { RecipeId = recipes[0].Id, IngredientId = ingredients.First(i => i.Name == "Eggs").Id },
                new RecipeIngredient
                    { RecipeId = recipes[0].Id, IngredientId = ingredients.First(i => i.Name == "Flour").Id },
                new RecipeIngredient
                    { RecipeId = recipes[0].Id, IngredientId = ingredients.First(i => i.Name == "Milk").Id },

                // Spaghetti Bolognese ingredients
                new RecipeIngredient
                    { RecipeId = recipes[1].Id, IngredientId = ingredients.First(i => i.Name == "Pasta").Id },
                new RecipeIngredient
                    { RecipeId = recipes[1].Id, IngredientId = ingredients.First(i => i.Name == "Tomato").Id },

                // Pasta Marinara ingredients
                new RecipeIngredient
                    { RecipeId = recipes[2].Id, IngredientId = ingredients.First(i => i.Name == "Tomato").Id },
                new RecipeIngredient
                    { RecipeId = recipes[2].Id, IngredientId = ingredients.First(i => i.Name == "Pasta").Id },
            };
            context.RecipeIngredients.AddRange(recipeIngredients);

            // Seed Tags
            var recipeTags = new List<RecipeTag>
            {
                // Pancakes tags
                new RecipeTag { RecipeId = recipes[0].Id, TagId = tags.First(t => t.Name == "Vegetarian").Id },
                new RecipeTag { RecipeId = recipes[0].Id, TagId = tags.First(t => t.Name == "Quick").Id },

                // Spaghetti Bolognese tags
                new RecipeTag { RecipeId = recipes[1].Id, TagId = tags.First(t => t.Name == "Spicy").Id },

                // Pasta Marinara tags
                new RecipeTag { RecipeId = recipes[2].Id, TagId = tags.First(t => t.Name == "Vegetarian").Id }
            };
            context.RecipeTags.AddRange(recipeTags);

            context.SaveChanges();
        }
    }
}
