using JetBrains.Annotations;

namespace SenseEvents.Features.Events.GetEvents;

/// <summary>
/// Модель ответа на запрос всех мероприятий.
/// </summary>
public class GetEventsResponse
{
    /// <summary>
    /// Коллекция существующих мероприятий.
    /// </summary>
    [UsedImplicitly] // json serialization
    public IEnumerable<Event> Events { get; set; } = null!;
}