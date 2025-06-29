using AutoMapper;
using Common.Responses;
using DomainModels;
using DTOs.CategoryDto;
using DTOs.IngredientDto;
using DTOs.RecipeDto;
using DTOs.TagDto;

namespace Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Recipe mappings
            /*
            CreateMap<Recipe, RecipeDto>()
                .ReverseMap()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (int)src.Difficulty))
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (DifficultyLevel)src.Difficulty));
            CreateMap<Recipe, AddRecipeDto>()
                .ReverseMap()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
            CreateMap<Recipe, UpdateRecipeDto>().ReverseMap();
                //.ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
            */
            // Recipe -> RecipeDto (with nested mappings)
            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (int)src.Difficulty))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.RecipeIngredients))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.RecipeTags))
                .ReverseMap()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (DifficultyLevel)src.Difficulty));

            CreateMap<AddRecipeDto, Recipe>()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (DifficultyLevel)src.Difficulty))
                .ForMember(dest => dest.RecipeIngredients, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeTags, opt => opt.Ignore());

            CreateMap<UpdateRecipeDto, Recipe>()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (DifficultyLevel)src.Difficulty))
                .ForMember(dest => dest.RecipeIngredients, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeTags, opt => opt.Ignore());

            // Ingredient join mapping
            CreateMap<RecipeIngredient, RecipeIngredientDto>()
                .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.IngredientId))
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ReverseMap()
                .ForMember(dest => dest.Ingredient, opt => opt.Ignore()) // handled manually
                .ForMember(dest => dest.Recipe, opt => opt.Ignore());    // handled manually
            
            // Tag join mapping
            CreateMap<RecipeTag, RecipeTagDto>()
                .ForMember(dest => dest.TagId, opt => opt.MapFrom(src => src.TagId))
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.Tag.Name))
                .ReverseMap()
                .ForMember(dest => dest.Recipe, opt => opt.Ignore())
                .ForMember(dest => dest.Tag, opt => opt.Ignore());

            // Ingredient mappings
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<Ingredient, AddIngredientDto>().ReverseMap();
            CreateMap<Ingredient, UpdateIngredientDto>().ReverseMap();

            //RecipeIngredient mappings
            //CreateMap<RecipeIngredient, RecipeIngredientDto>()
            //    .ForMember(dest => dest.IngredientName, opt =>
            //        opt.MapFrom(src => src.Ingredient.Name));
            //CreateMap<RecipeIngredientDto, RecipeIngredient>()
            //    .ForMember(dest => dest.Ingredient,
            //        opt => opt.Ignore()) // Will be loaded in service
            //    .ForMember(dest => dest.Recipe,
            //        opt => opt.Ignore());    // Set in service as well

            // Tag mappings
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Tag, AddTagDto>().ReverseMap();
            CreateMap<Tag, UpdateTagDto>().ReverseMap();

            // Category mappings
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Recipes,
                    opt => opt.MapFrom((
                        src => src.Recipes)))
                .ReverseMap();
            CreateMap<Category, AddCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap(typeof(CustomResponse<>), typeof(CustomResponse<>))
                .ConvertUsing(typeof(CustomResponseTypeConverter<,>));

            CreateMap<Category, CategorySimpleDto>().ReverseMap();
        }
    }
}
