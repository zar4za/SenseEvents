using MediatR;

namespace SenseEvents.Features.Events.DeleteEvent;

/// <summary>
/// Модель запроса для удаления мероприятия.
/// </summary>
public class DeleteEventCommand : IRequest<DeleteEventResponse>
{
    /// <summary>
    /// Идентификатор удаляемого мероприятия.
    /// </summary>
    public Guid Id { get; init; }
}