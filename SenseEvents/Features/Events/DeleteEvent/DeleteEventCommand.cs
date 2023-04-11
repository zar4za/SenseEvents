using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SenseEvents.Features.Events.DeleteEvent;

/// <summary>
/// Модель запроса для удаления мероприятия.
/// </summary>
public class DeleteEventCommand : IRequest<DeleteEventResponse>
{
    /// <summary>
    /// Идентификатор удаляемого мероприятия.
    /// </summary>
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}