using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.AddEvent
{
    [UsedImplicitly] //mediator
    public class AddEventHandler : IRequestHandler<AddEventCommand, AddEventResponse>
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
