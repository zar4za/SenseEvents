using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.AddEvent;

/// <summary>
/// Модель запроса для добавления нового мероприятия
/// </summary>
public class AddEventCommand : IRequest<AddEventResponse>
{
    /// <summary>
    /// Время начала мероприятия.
    /// </summary>
    [UsedImplicitly] // json parsing
    public DateTime StartUtc { get; init; }

    /// <summary>
    /// Время окончания мероприятия. Должно быть позже времени начала.
    /// </summary>
    [UsedImplicitly] // json parsing
        
    public DateTime EndUtc { get; init; }

    /// <summary>
    /// Название мероприятия.
    /// </summary>
    [UsedImplicitly] // json parsing
    public string Name { get; init; } = null!;

    /// <summary>
    /// Краткое описание мероприятия.
    /// </summary>
    [UsedImplicitly] // json parsing
    public string? Description { get; init; }

    /// <summary>
    /// Идентификатор картинки для шапки мероприятия.
    /// </summary>
    [UsedImplicitly] // json parsing
    public Guid ImageId { get; init; }

    /// <summary>
    /// Идентификатор пространства, в котором будет проходить мероприятие.
    /// </summary>
    [UsedImplicitly] // json parsing
    public Guid SpaceId { get; init; }

    /// <summary>
    /// Максимальное количество билетов, которое можно выдать на мероприятие.
    /// </summary>
    [UsedImplicitly] // json parsing 
    public int? MaxTickets { get; init; }

    /// <summary>
    /// Цена за билет на мероприятие
    /// </summary>
    public decimal TicketPrice { get; init; }
}