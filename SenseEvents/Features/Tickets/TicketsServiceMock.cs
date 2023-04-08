namespace SenseEvents.Features.Tickets
{
    public class TicketsServiceMock : ITicketsService
    {
        public Ticket AddTicket(Guid eventId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ticket> GetTickets(Guid eventId)
        {
            throw new NotImplementedException();
        }
    }
}
