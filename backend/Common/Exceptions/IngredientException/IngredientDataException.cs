namespace Common.Exceptions.IngredientException
{
    public class IngredientDataException : Exception
    {
        public IngredientDataException(string message) : base(message)
        {
        }

        public IngredientDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
