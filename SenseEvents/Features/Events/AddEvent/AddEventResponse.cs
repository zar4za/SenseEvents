using JetBrains.Annotations;

namespace SenseEvents.Features.Events.AddEvent;

/// <summary>
/// Модель ответа на добавление мероприятия.
/// </summary>
public class AddEventResponse
{
    /// <summary>
    /// Идентификатор, присвоенный добавленному мероприятию.
    /// </summary>
    [UsedImplicitly] // json serialization
    public Guid Id { get; set; }
}