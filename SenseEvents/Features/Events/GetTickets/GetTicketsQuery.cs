using MediatR;

namespace SenseEvents.Features.Events.GetTickets
{
    public class GetTicketsQuery : IRequest<GetTicketsResponse>
    {
        public Guid EventId { get; set; }
    }
}
