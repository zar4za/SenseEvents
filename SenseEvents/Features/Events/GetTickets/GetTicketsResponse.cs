using SenseEvents.Features.Tickets;

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
        public IEnumerable<Ticket> Tickets { get; init; } = null!;
    }
}
