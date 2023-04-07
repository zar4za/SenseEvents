namespace SenseEvents.Infrastructure.Identity
{
    public class SpaceServiceMock : ISpaceService
    {
        public bool SpaceExists(Guid id)
        {
            return true;
        }
    }
}
