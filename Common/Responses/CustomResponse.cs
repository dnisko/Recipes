using System.Collections;

namespace Common.Responses
{
    /*
    public class CustomResponse
    {
        public bool IsSuccessful { get; set; }
        public IEnumerable<string> Errors { get; protected set; } = new List<string>();

        public CustomResponse(params string[] errors) => Errors = errors;
        public CustomResponse(IEnumerable<string> errors) => Errors = errors;
        public CustomResponse(bool isSuccessful) => IsSuccessful = isSuccessful;

        public static CustomResponse Success => new(true);
    }

    public class CustomResponse<TResult> : CustomResponse where TResult : new()
    {
        public TResult? Result { get; set; } = default;

        public CustomResponse(TResult? result)
        {
            IsSuccessful = true;
            Result = result;
        }

        public CustomResponse(params string[] errors) : base(errors)
        {
        }

        public CustomResponse(IEnumerable<string> errors) : base(errors)
        {
        }

        public static CustomResponse<List<T>> HandleEmptyList<T>(IEnumerable<T> data, string logMessage, ILogger logger)
        {
            if (data == null || !data.Any())
            {
                logger.LogError(logMessage);
                return new CustomResponse<List<T>>(logMessage);
            }
            return new CustomResponse<List<T>>(data.ToList());
        }
    }
    */

    public class CustomResponse
    {
        public bool IsSuccessful { get; private set; }
        public IReadOnlyList<string> Errors { get; private set; }

        protected CustomResponse(bool isSuccessful, IEnumerable<string>? errors = null)
        {
            IsSuccessful = isSuccessful;
            Errors = errors?.ToList() ?? new List<string>();
        }

        public static CustomResponse Success() => new(true);

        public static CustomResponse Fail(params string[] errors) => new(false, errors);

        public static CustomResponse Fail(IEnumerable<string> errors) => new(false, errors);
    }

    public class CustomResponse<TResult> : CustomResponse
    {
        public TResult? Result { get; private set; }

        private CustomResponse(TResult result) : base(true)
        {
            Result = result;
        }

        private CustomResponse(IEnumerable<string> errors) : base(false, errors)
        {
            Result = default;
        }

        public static CustomResponse<TResult> Success(TResult result) => new(result);

        public static CustomResponse<TResult> Fail(params string[] errors) => new(errors);

        public static CustomResponse<TResult> Fail(IEnumerable<string> errors) => new(errors);

        public static CustomResponse<List<T>> FromList<T>(IEnumerable<T>? data, string errorMessageIfEmpty)
        {
            if (data == null || !data.Any())
            {
                return Fail<List<T>>(errorMessageIfEmpty);
            }

            return Success(data.ToList());
        }
    }
}
