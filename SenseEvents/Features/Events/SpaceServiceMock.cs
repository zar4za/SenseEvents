namespace SenseEvents.Features.Events
{
    public class SpaceServiceMock : ISpaceService
    {
        public bool SpaceExists(Guid id)
        {
            return true;
        }
    }
}
