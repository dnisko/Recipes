using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        //public DbSet<Image> Images { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        //public DbSet<RecipeTag> RecipeTags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Ignore<BaseEntity>();

            // Recipe -> Tags (Many-to-Many)
            builder.Entity<Recipe>()
                .HasMany(r => r.Tags)
                .WithMany(t => t.Recipes)
                .UsingEntity(j => j.ToTable("RecipeTags"));

            // Recipe -> Ingredients (One-to-Many)
            builder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
