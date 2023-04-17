namespace SenseEvents.Infrastructure.Services.Spaces;

public interface ISpaceService
{
    bool SpaceExists(Guid id);
}