namespace SenseEvents.Infrastructure.Services.Images;

public interface IImageService
{
    bool ImageExists(Guid id);
}