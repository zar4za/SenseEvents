using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.AddEvent
{
    /// <summary>
    /// Модель запроса для добавления нового мероприятия
    /// </summary>
    public class AddEventCommand : IRequest<AddEventResponse>
    {
        /// <summary>
        /// Время начала мероприятия.
        /// </summary>
        public DateTime StartUtc { get; init; }

        /// <summary>
        /// Время окончания мероприятия. Должно быть позже времени начала.
        /// </summary>
        public DateTime EndUtc { get; init; }

        /// <summary>
        /// Название мероприятия.
        /// </summary>
        [UsedImplicitly] // json parsing
        public string Name { get; init; } = null!;

        /// <summary>
        /// Краткое описание мероприятия.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// Идентификатор картинки для шапки мероприятия.
        /// </summary>
        public Guid ImageId { get; init; }

        /// <summary>
        /// Идентификатор пространства, в котором будет проходить мероприятие.
        /// </summary>
        public Guid SpaceId { get; init; }
    }
}
