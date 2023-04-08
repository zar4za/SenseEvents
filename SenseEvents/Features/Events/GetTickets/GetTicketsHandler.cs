using MediatR;
using SenseEvents.Features.Tickets;

namespace SenseEvents.Features.Events.GetTickets
{
    public class GetTicketsHandler : IRequestHandler<GetTicketsQuery, GetTicketsResponse>
    {
        private ITicketsService _ticketsService;

        public GetTicketsHandler(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        public async Task<GetTicketsResponse> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var tickets = _ticketsService.GetTickets(request.EventId);

            return new GetTicketsResponse()
            {
                Tickets = tickets
            };
        }
    }
}
