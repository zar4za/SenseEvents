using SenseEvents.Features.Tickets;

namespace SenseEvents.Features.Events.GetTickets
{
    public class GetTicketsResponse
    {
        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
