using JetBrains.Annotations;

namespace SenseEvents.Features.Events.UpdateEvent;

/// <summary>
/// Модель ответа на обновление информации о мероприятии.
/// </summary>
public class UpdateEventResponse
{
    /// <summary>
    /// Флаг, показывающий успешность обновления мероприятия.
    /// </summary>
    [UsedImplicitly] // json serialization
    public bool Success { get; set; }
}