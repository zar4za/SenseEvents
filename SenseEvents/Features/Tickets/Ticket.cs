using JetBrains.Annotations;

namespace SenseEvents.Features.Tickets;

/// <summary>
/// Модель билета на мероприятие.
/// </summary>
public class Ticket
{
    /// <summary>
    /// Идентификатор билета.
    /// </summary>
    [UsedImplicitly] // json serialization
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор владельца билета.
    /// </summary>
    [UsedImplicitly] // json serialization
    public Guid OwnerId { get; set; }

    /// <summary>
    /// Номер места в пространстве. Может не указываться.
    /// </summary>
    [UsedImplicitly] // json serialization
    public int Seat { get; set; }
}