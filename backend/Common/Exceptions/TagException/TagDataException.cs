namespace Common.Exceptions.TagException
{
    public class TagDataException : Exception
    {
        public TagDataException(string message) : base(message)
        {
        }
        public TagDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
