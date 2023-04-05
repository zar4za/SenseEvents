namespace SenseEvents.Features.Events
{
    public class ImageServiceMock : IImageService
    {
        public bool ImageExists(Guid id)
        {
            return true;
        }
    }
}
