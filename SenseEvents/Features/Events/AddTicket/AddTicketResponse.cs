namespace SenseEvents.Features.Events.AddTicket
{
    /// <summary>
    /// Модель ответа на выдачу билета на мероприятие.
    /// </summary>
    public class AddTicketResponse
    {
        /// <summary>
        /// Выданный билет.
        /// </summary>
        public Ticket Ticket { get; init; } = null!;
    }
}
