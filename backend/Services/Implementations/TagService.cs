using AutoMapper;
using Common.Exceptions.TagException;
using Common.Responses;
using DataAccess.Interfaces;
using DTOs.TagDto;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ILogger<ITagService> _logger;
        private readonly IMapper _mapper;
        public TagService(ITagRepository tagRepository, ILogger<ITagService> logger, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<CustomResponse<List<TagDto>>> GetPopularTagsAsync()
        {
            try
            {
                var tags = await _tagRepository.GetPopularTags();
                if (tags == null || !tags.Any())
                {
                    _logger.LogError($"No tags found.");
                    return new CustomResponse<List<TagDto>>($"No tags found.");
                }
                var tagDto = _mapper.Map<List<TagDto>>(tags);
                return new CustomResponse<List<TagDto>>(tagDto);
            }
            catch (TagDataException ex)
            {
                throw new TagDataException($"Error while getting the categories: {ex.Message}");
            }
        }

        public async Task<CustomResponse<List<TagDto>>> GetTagsByARecipeAsync(int id)
        {
            try
            {
                var tags = await _tagRepository.GetTagsByRecipe(id);
                if (tags == null || !tags.Any())
                {
                    _logger.LogError($"No tags found.");
                    return new CustomResponse<List<TagDto>>($"No tags found.");
                }
                var tagDto = _mapper.Map<List<TagDto>>(tags);
                return new CustomResponse<List<TagDto>>(tagDto);
            }
            catch (TagDataException ex)
            {
                throw new TagDataException($"Error while getting the categories: {ex.Message}");
            }
        }
    }
}
