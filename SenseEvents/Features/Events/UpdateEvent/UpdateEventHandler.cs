using MediatR;

namespace SenseEvents.Features.Events.UpdateEvent
{
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, UpdateEventResponse>
    {
        private readonly IEventsService _eventsService;

        public UpdateEventHandler(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }


        public async Task<UpdateEventResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            var storedEvent = await _eventsService.GetEvent(request.Id);
            
            storedEvent.StartUtc = request.StartUtc;
            storedEvent.EndUtc = request.EndUtc;
            storedEvent.Description = request.Description;
            storedEvent.Name = request.Name;
            storedEvent.ImageId = request.ImageId;
            storedEvent.SpaceId = request.SpaceId;

            return new UpdateEventResponse()
            {
                Success = true
            };
        }
    }
}
