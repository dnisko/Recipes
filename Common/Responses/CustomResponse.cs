namespace Common.Responses
{
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
    }
}
