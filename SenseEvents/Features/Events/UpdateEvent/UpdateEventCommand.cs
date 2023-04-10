using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.UpdateEvent;

/// <summary>
/// Модель запроса для обновления информации о мероприятии.
/// </summary>
public class UpdateEventCommand : IRequest<UpdateEventResponse>
{
    /// <summary>
    /// Идентификатор изменяемого мероприятия.
    /// </summary>
    [JsonIgnore]
    [UsedImplicitly] // json parsing
    public Guid Id { get; set; }

    /// <summary>
    /// Время начала мероприятия.
    /// </summary>
    [UsedImplicitly] // json parsing
    public DateTime StartUtc { get; set; }

    /// <summary>
    /// Время окончания мероприятия. Должно быть позже времени начала.
    /// </summary>
    [UsedImplicitly] // json parsing
    public DateTime EndUtc { get; set; }

    /// <summary>
    /// Название мероприятия.
    /// </summary>
    [UsedImplicitly] // json parsing
    public string Name { get; set; } = null!;

    /// <summary>
    /// Краткое описание мероприятия.
    /// </summary>
    [UsedImplicitly] // json parsing
    public string? Description { get; set; }

    /// <summary>
    /// Идентификатор картинки для шапки мероприятия.
    /// </summary>
    [UsedImplicitly] // json parsing
    public Guid ImageId { get; set; }

    /// <summary>
    /// Идентификатор пространства, в котором будет проходить мероприятие.
    /// </summary>
    [UsedImplicitly] // json parsing
    public Guid SpaceId { get; set; }
}