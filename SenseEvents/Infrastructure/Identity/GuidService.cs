namespace SenseEvents.Infrastructure.Identity
{
    public class GuidService : IGuidService
    {
        public Guid GetNewId() => Guid.NewGuid();
    }
}
