using System.Text.Json.Serialization;
using MediatR;

namespace SenseEvents.Features.Events.UpdateEvent
{
    /// <summary>
    /// Модель запроса для обновления информации о мероприятии.
    /// </summary>
    public class UpdateEventCommand : IRequest<UpdateEventResponse>
    {
        /// <summary>
        /// Идентификатор изменяемого мероприятия.
        /// </summary>
        [JsonIgnore]
        public Guid Id { get; set; }

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
        public string Name { get; set; }

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

