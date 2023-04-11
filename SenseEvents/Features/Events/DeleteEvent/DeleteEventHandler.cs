using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.DeleteEvent;

[UsedImplicitly] // Mediator
public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, DeleteEventResponse>
{
    private readonly IEventsService _eventsService;

    public DeleteEventHandler(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    public async Task<DeleteEventResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var success = await  _eventsService.DeleteEvent(request.Id);
        return new DeleteEventResponse
        {
            Success = success
        };
    }
}