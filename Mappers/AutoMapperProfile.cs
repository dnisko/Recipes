using AutoMapper;
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
            CreateMap<Recipe, RecipeDto>()
                .ReverseMap()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
            CreateMap<Recipe, AddRecipeDto>()
                .ReverseMap()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
            CreateMap<Recipe, UpdateRecipeDto>().ReverseMap();
                //.ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients))
                //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            // Ingredient mappings
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
            CreateMap<Ingredient, AddIngredientDto>().ReverseMap();
            CreateMap<Ingredient, UpdateIngredientDto>().ReverseMap();

            // Tag mappings
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<Tag, AddTagDto>().ReverseMap();
            CreateMap<Tag, UpdateTagDto>().ReverseMap();

            // Category mappings
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, AddCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        }
    }
}
