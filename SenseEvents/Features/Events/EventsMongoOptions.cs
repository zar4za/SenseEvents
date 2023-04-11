namespace SenseEvents.Features.Events
{
    public class EventsMongoOptions
    {
        public string ConnectionString { get; init; } = null!;
        public string DatabaseName { get; init; } = null!;
        public string EventsCollectionName { get; init; } = null!;
    }
}
