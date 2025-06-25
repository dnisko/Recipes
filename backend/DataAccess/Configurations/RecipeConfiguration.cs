using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            //builder.HasMany(r => r.Tags)
            //    .WithMany(t => t.Recipes)
            //    .UsingEntity<RecipeTag>(
            //        j => j.ToTable("RecipeTags"));

            //builder.HasIndex(r => r.Name).IsUnique();
            //builder.HasMany(r => r.Tags)
                //.WithMany(t => t.Recipes)
                //.UsingEntity<RecipeTag>(
                //    j => j.ToTable("RecipeTags")
                //        .HasOne(rt => rt.Tag)
                //        .WithMany()
                //        .OnDelete(DeleteBehavior.NoAction), // Specify NoAction for the Tag relationship
                //    j => j.HasOne(rt => rt.Recipe)
                //        .WithMany()
                //        .OnDelete(DeleteBehavior.NoAction) // Specify NoAction for the Recipe relationship
                //);
                ;

            builder.HasIndex(r => r.Name).IsUnique();
        }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
