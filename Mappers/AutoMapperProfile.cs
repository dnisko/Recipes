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
            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (int)src.Difficulty))
                .ReverseMap()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (DifficultyLevel)src.Difficulty));

            CreateMap<Recipe, AddRecipeDto>()
                .ReverseMap()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (DifficultyLevel)src.Difficulty));

            CreateMap<Recipe, UpdateRecipeDto>()
                .ReverseMap()
                .ForMember(dest => dest.Difficulty, opt => opt.MapFrom(src => (DifficultyLevel)src.Difficulty));
            
            // Ingredient mappings
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<Ingredient, AddIngredientDto>().ReverseMap();
            CreateMap<Ingredient, UpdateIngredientDto>().ReverseMap();

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
        }
    }
}
