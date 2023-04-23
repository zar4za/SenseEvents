using JetBrains.Annotations;
using SenseEvents.Features.Tickets;
using System.Text.Json.Serialization;

namespace SenseEvents.Features.Events;

/// <summary>
/// Модель, содержащая информацию о мероприятии.
/// </summary>
public class Event
{
    private readonly List<Ticket> _tickets = null!;

    /// <summary>
    /// Уникальный идентификатор мероприятия.
    /// </summary>
    [UsedImplicitly] // json serialization
    public Guid Id { get; set; }

    /// <summary>
    /// Время начала мероприятия.
    /// </summary>
    [UsedImplicitly] // json serialization
    public DateTime StartUtc { get; set; }

    /// <summary>
    /// Время окончания мероприятия. Должно быть позже времени начала.
    /// </summary>
    [UsedImplicitly] // json serialization
    public DateTime EndUtc { get; set; }

    /// <summary>
    /// Название мероприятия.
    /// </summary>
    [UsedImplicitly] // json serialization
    public string Name { get; set; } = null!;

    /// <summary>
    /// Краткое описание мероприятия.
    /// </summary>
    [UsedImplicitly] // json serialization
    public string? Description { get; set; }

    /// <summary>
    /// Идентификатор картинки для шапки мероприятия.
    /// </summary>
    [UsedImplicitly] // json serialization
    public Guid ImageId { get; set; }

    /// <summary>
    /// Идентификатор пространства, в котором будет проходить мероприятие.
    /// </summary>
    [UsedImplicitly] // json serialization
    public Guid SpaceId { get; set; }

    [JsonIgnore]
    public int MaxTickets { private get; init; }

    /// <summary>
    /// Коллекция выданных билетов на это мероприятие.
    /// </summary>
    public IEnumerable<Ticket> Tickets
    {
        get => _tickets;
        init => _tickets = value.ToList();
    }

    /// <summary>
    /// Можно ли выпустить еще один билет на данное мероприятие.
    /// </summary>
    [UsedImplicitly]
    public bool CanIssueTicket => Tickets.Count() < MaxTickets;

    /// <summary>
    /// Стоимость билета
    /// </summary>
    public decimal TicketPrice { get; set; }
}