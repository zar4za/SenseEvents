using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.AddTicket;

/// <summary>
/// Модель запроса для выдачи билета на мероприятие.
/// </summary>
public class AddTicketCommand : IRequest<AddTicketResponse>
{
    /// <summary>
    /// Уникальный идентификатор мероприятия.
    /// </summary>

    [JsonIgnore]
    public Guid EventId { get; set; }

    /// <summary>
    /// Идентификатор владельца билета.
    /// </summary>
    [UsedImplicitly] // json parsing
    public Guid OwnerId { get; init; }

    /// <summary>
    /// Номер места в пространстве.
    /// </summary>
    [UsedImplicitly] // json parsing
    public int? Seat { get; init; }
}