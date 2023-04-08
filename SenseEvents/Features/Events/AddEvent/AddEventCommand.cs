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
        public DateTime StartUtc { get; set; }

        /// <summary>
        /// Время окончания мероприятия. Должно быть позже времени начала.
        /// </summary>
        public DateTime EndUtc { get; set; }

        /// <summary>
        /// Название мероприятия.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Краткое описание мероприятия.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Идентификатор картинки для шапки мероприятия.
        /// </summary>
        public Guid ImageId { get; set; }

        /// <summary>
        /// Идентификатор пространства, в котором будет проходить мероприятие.
        /// </summary>
        public Guid SpaceId { get; set; }
    }
}
