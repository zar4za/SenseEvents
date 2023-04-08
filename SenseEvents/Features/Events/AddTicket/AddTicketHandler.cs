using MediatR;
using SenseEvents.Features.Tickets;
using SenseEvents.Infrastructure.Identity;

namespace SenseEvents.Features.Events.AddTicket
{
    public class AddTicketHandler : IRequestHandler<AddTicketCommand, AddTicketResponse>
    {
        private readonly IGuidService _guidService;
        private readonly IEventsService _eventsService;

        public AddTicketHandler(IGuidService guidService, IEventsService eventsService)
        {
            _guidService = guidService;
            _eventsService = eventsService;
        }

        public async Task<AddTicketResponse> Handle(AddTicketCommand request, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                Id = _guidService.GetNewId(),
                OwnerId = request.OwnerId,
                Seat = request.Seat
            };

            await _eventsService.AddTicket(request.EventId, ticket);

            return new AddTicketResponse()
            {
                Ticket = ticket
            };
        }
    }
}
