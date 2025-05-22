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
            // Recipe mappings
            CreateMap<Recipe, RecipeDto>()
                .ReverseMap()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
            CreateMap<AddRecipeDto, Recipe>()
                .ReverseMap()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
            CreateMap<UpdateRecipeDto, Recipe>().ReverseMap();
                //.ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            // Ingredient mappings
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<IngredientDto, Ingredient>().ReverseMap();

            // Tag mappings
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<TagDto, Tag>().ReverseMap();

            // Category mappings
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
        }
    }
}
