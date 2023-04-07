namespace SenseEvents.Infrastructure.Identity
{
    public interface IImageService
    {
        bool ImageExists(Guid id);
    }
}
