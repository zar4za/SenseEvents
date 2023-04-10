using JetBrains.Annotations;

namespace SenseEvents.Features.Events.GetTickets
{
    /// <summary>
    /// Модель ответа на запрос всех билетов на мероприятие.
    /// </summary>
    public class GetTicketsResponse
    {
        /// <summary>
        /// Коллекция выданных билетов на мероприятие.
        /// </summary>
        [UsedImplicitly] // json serialization
        public IEnumerable<Ticket> Tickets { get; init; } = null!;
    }
}
