namespace SenseEvents.Features.Id
{
    public class GuidService : IGuidService
    {
        public Guid GetNewId() => Guid.NewGuid();
    }
}
