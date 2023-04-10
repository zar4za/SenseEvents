using JetBrains.Annotations;

namespace SenseEvents.Features.Events
{
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
        public Guid Id { get; init; }

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

        /// <summary>
        /// Коллекция выданных билетов на это мероприятие.
        /// </summary>
        public IEnumerable<Ticket> Tickets
        {
            get => _tickets;
            init => _tickets = value.ToList();
        }

        /// <summary>
        /// Добавляет билет в коллекцию билетов.
        /// </summary>
        /// <param name="ticket">Билет, который будет добавлен.</param>
        public void AddTicket(Ticket ticket)
        {
            _tickets.Add(ticket);
        }
    }
}
