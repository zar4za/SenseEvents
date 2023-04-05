using SenseEvents.Infrastructure.Messaging;

namespace SenseEvents.Features.Events.DeleteEvent
{
    public class DeleteEventHandler : ICommandHandler<DeleteEventCommand, DeleteEventResponse>
    {
        private readonly IEventsService _eventsService;

        public DeleteEventHandler(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public async Task<DeleteEventResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var success = await  _eventsService.DeleteEvent(request);
            return new DeleteEventResponse()
            {
                Success = success
            };
        }
    }
}
