using SenseEvents.Infrastructure.Messaging;

namespace SenseEvents.Features.Events.AddEvent
{
    public class AddEventHandler : ICommandHandler<AddEventCommand, AddEventResponse>
    {
        private readonly IEventsService _eventsService;

        public AddEventHandler(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public async Task<AddEventResponse> Handle(AddEventCommand command, CancellationToken cancellationToken)
        {
            var id = await _eventsService.AddEvent(command);

            return new AddEventResponse()
            {
                Id = id
            };
        }
    }
}
