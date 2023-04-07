namespace SenseEvents.Infrastructure.Identity
{
    public class ImageServiceMock : IImageService
    {
        public bool ImageExists(Guid id)
        {
            return true;
        }
    }
}
