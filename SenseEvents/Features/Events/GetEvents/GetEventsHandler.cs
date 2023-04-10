using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.GetEvents;

[UsedImplicitly] // Mediator
public class GetEventsHandler : IRequestHandler<GetEventsQuery, GetEventsResponse>
{
    private readonly IEventsService _eventsService;

    public GetEventsHandler(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    public async Task<GetEventsResponse> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        return new GetEventsResponse
        {
            Events = await _eventsService.GetEvents()
        };
    }
}