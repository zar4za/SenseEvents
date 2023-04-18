using JetBrains.Annotations;
using MediatR;
using SenseEvents.Features.Events;

namespace SenseEvents.Infrastructure.RabbitMQ.Events.SpaceDelete;

[UsedImplicitly]
public class SpaceDeleteHandler : IRequestHandler<SpaceDeleteEvent>
{
    private readonly IEventsService _eventsService;

    public SpaceDeleteHandler(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }


    public async Task Handle(SpaceDeleteEvent request, CancellationToken cancellationToken)
    {
        await _eventsService.DeleteEventsInSpace(request.SpaceId);
    }
}