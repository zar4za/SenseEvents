using JetBrains.Annotations;
using MediatR;
using SenseEvents.Features.Events;

namespace SenseEvents.Infrastructure.RabbitMQ.Events.ImageDelete;

[UsedImplicitly]
public class ImageDeleteHandler : IRequestHandler<ImageDeleteEvent>
{
    private readonly IEventsService _eventsService;

    public ImageDeleteHandler(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    public async Task Handle(ImageDeleteEvent request, CancellationToken cancellationToken)
    {
        await _eventsService.UpdateImage(request.ImageId, null);
    }
}