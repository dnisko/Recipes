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
        public bool IsSuccessful { get; protected set; }
        public IEnumerable<string> Errors { get; protected set; }
        public IEnumerable<string> SuccessMessage { get; protected set; }

        protected CustomResponse(
            bool isSuccessful,
            IEnumerable<string>? errors = null,
            IEnumerable<string>? successMessage = null)
        {
            IsSuccessful = isSuccessful;
            Errors = errors ?? new List<string>();
            SuccessMessage = successMessage ?? new List<string>();
        }

        public static CustomResponse Success(params string[] successMessages)
            => new CustomResponse(true, null, successMessages);

        public static CustomResponse Fail(params string[] errors)
            => new CustomResponse(false, errors);

        public static CustomResponse Fail(IEnumerable<string> errors)
            => new CustomResponse(false, errors);
    }

    public class CustomResponse<TResult> : CustomResponse
    {
        public TResult? Result { get; private set; }

        private CustomResponse(
            TResult result, 
            IEnumerable<string>? successMessages = null) 
            : base(true, null, successMessages)
        {
            Result = result;
        }

        private CustomResponse(
            IEnumerable<string> errors,
            IEnumerable<string>? successMessage = null)
            : base(false, errors, successMessage)
        {
            Result = default;
        }


        public static CustomResponse<TResult> Success(TResult result, params string[] successMessages)
            => new CustomResponse<TResult>(result, successMessages);

        public static new CustomResponse<TResult> Fail(params string[] errors)
            => new CustomResponse<TResult>(errors);

        public static new CustomResponse<TResult> Fail(IEnumerable<string> errors)
            => new CustomResponse<TResult>(errors);

        public static CustomResponse<TResult> CreateFailed(IEnumerable<string> errors, IEnumerable<string>? successMessages = null)
        {
            return new CustomResponse<TResult>(errors, successMessages);
        }
    }

    public static class CustomResponseFactory
    {
        public static CustomResponse<List<T>> FromList<T>(IEnumerable<T>? data, string errorMessageIfEmpty)
        {
            if (data == null || !data.Any())
            {
                return CustomResponse<List<T>>.Fail(errorMessageIfEmpty);
            }
            return CustomResponse<List<T>>.Success(data.ToList());
        }
    }

}
