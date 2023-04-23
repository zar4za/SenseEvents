using MassTransit;
using SenseEvents.Features.Events;

namespace SenseEvents.Infrastructure.RabbitMQ.Events.SpaceDelete;

public class SpaceDeleteEventConsumer : IConsumer<SpaceDeleteEvent>
{
    private readonly IEventsService _eventsService;

    public SpaceDeleteEventConsumer(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }

    public async Task Consume(ConsumeContext<SpaceDeleteEvent> context)
    {
        await _eventsService.DeleteEventsInSpace(context.Message.SpaceId);
    }
}