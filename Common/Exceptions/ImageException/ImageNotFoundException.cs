namespace Common.Exceptions.ImageException
{
    public class ImageNotFoundException : Exception
    {
        public ImageNotFoundException(string message) : base(message)
        {
        }
        public ImageNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public ImageNotFoundException(string message, string imagePath) : base($"{message} - Image Path: {imagePath}")
        {
        }
        public ImageNotFoundException(string message, string imagePath, Exception innerException) : base($"{message} - Image Path: {imagePath}", innerException)
        {
        }
    }
}
