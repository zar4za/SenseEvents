namespace SenseEvents.Features.Tickets
{
    public interface ITicketsService
    {
        Ticket AddTicket(Guid eventId);
        IEnumerable<Ticket> GetTickets(Guid eventId);
    }
}
