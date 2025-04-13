namespace Common.Exceptions.UserException
{
    public class UserDataException : Exception
    {
        public UserDataException(string message) : base(message)
        {
        }
        public UserDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
