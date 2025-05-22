using AutoMapper;
using DomainModels;
using DTOs.CategoryDto;
using DTOs.ImageDto;
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
            CreateMap<Category, AddCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Image, AddImageDto>().ReverseMap();
            CreateMap<Image, UpdateImageDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();

            CreateMap<Ingredient, AddIngredientDto>().ReverseMap();
            CreateMap<Ingredient, UpdateIngredientDto>().ReverseMap();
            CreateMap<Ingredient, IngredientDto>().ReverseMap();

            CreateMap<Recipe, AddRecipeDto>()
                .ForMember(dest => dest.Difficulty,
                    opt => 
                        opt.MapFrom
                            ((src => 
                                (DifficultyLevel)src.Difficulty)))
                .ReverseMap();
            CreateMap<Recipe, UpdateRecipeDto>().ReverseMap();
            CreateMap<Ingredient, RecipeDto>().ReverseMap();

            CreateMap<Tag, AddTagDto>().ReverseMap();
            CreateMap<Recipe, UpdateTagDto>().ReverseMap();
            CreateMap<Recipe, TagDto>().ReverseMap();

            CreateMap<User, AddUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
