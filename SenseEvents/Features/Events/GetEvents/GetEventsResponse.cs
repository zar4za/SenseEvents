namespace SenseEvents.Features.Events.GetEvents
{
    public class GetEventsResponse
    {
        public IEnumerable<Event> Events { get; set; } = null!;
    }
}
