namespace SenseEvents.Features.Events
{
    public class EventsMongoOptions
    {
        public const string ConfigSection = "Mongo";
        public string ConnectionString { get; init; } = null!;
        public string DatabaseName { get; init; } = null!;
        public string EventsCollectionName { get; init; } = null!;
    }
}
