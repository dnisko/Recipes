using AutoMapper;
using Common.Responses;

namespace Mappers
{
    public class CustomResponseTypeConverter<TSource, TDest> : ITypeConverter<CustomResponse<TSource>, CustomResponse<TDest>>
    {
        public CustomResponse<TDest> Convert(CustomResponse<TSource> source, CustomResponse<TDest> destination, ResolutionContext context)
        {
            if (source == null)
                return null!;

            var destResult = source.Result == null
                ? default(TDest)
                : context.Mapper.Map<TDest>(source.Result);

            if (source.IsSuccessful)
            {
                return CustomResponse<TDest>.Success(destResult);
            }
            else
            {
                // Use the internal factory method to create a failed response
                return CustomResponse<TDest>.CreateFailed(source.Errors, source.SuccessMessage);
            }
        }
    }
}