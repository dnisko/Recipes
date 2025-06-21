namespace Common.Exceptions.CategoryException
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(string message) : base(message)
        {
        }
        public CategoryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
