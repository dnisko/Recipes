using AutoMapper;
using Common.Responses;
using DomainModels;
using DomainModels.Enums;
using DTOs.CategoryDto;
using DTOs.IngredientDto;
using DTOs.RecipeDto;
using DTOs.TagDto;
using DTOs.UserDto;

namespace Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
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

            //User mappings
            CreateMap<User, LoginUserDto>().ReverseMap();
            CreateMap<User, LoginUserResponseDto>().ReverseMap();
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<User, RegisterUserResponseDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserResponseDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserResponseDto>()
                //.ForMember(dest => dest.Role, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
