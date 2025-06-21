namespace Common.Exceptions.ServerException
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message) : base(message)
        {
        }
    }
}
