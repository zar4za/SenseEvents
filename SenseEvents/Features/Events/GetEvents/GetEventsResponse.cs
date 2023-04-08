namespace SenseEvents.Features.Events.GetEvents
{
    /// <summary>
    /// Модель ответа на запрос всех мероприятий.
    /// </summary>
    public class GetEventsResponse
    {
        /// <summary>
        /// Коллекция существующих мероприятий.
        /// </summary>
        public IEnumerable<Event> Events { get; set; } = null!;
    }
}
