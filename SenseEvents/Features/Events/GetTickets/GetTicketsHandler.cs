using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.GetTickets
{
    [UsedImplicitly] // Mediator
    public class GetTicketsHandler : IRequestHandler<GetTicketsQuery, GetTicketsResponse>
    {
        private readonly IEventsService _eventsService;

        public GetTicketsHandler(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public async Task<GetTicketsResponse> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
        {
            var plannedEvent = await _eventsService.GetEvent(request.EventId);

            return new GetTicketsResponse()
            {
                Tickets = plannedEvent.Tickets
            };
        }
    }
}
