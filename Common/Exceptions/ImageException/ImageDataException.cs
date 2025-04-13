namespace Common.Exceptions.ImageException
{
    public class ImageDataException : Exception
    {
        public ImageDataException(string message) : base(message)
        {
        }
        public ImageDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
