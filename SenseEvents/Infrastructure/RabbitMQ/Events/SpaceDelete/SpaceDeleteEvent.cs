using MediatR;

namespace SenseEvents.Infrastructure.RabbitMQ.Events.SpaceDelete;

public class SpaceDeleteEvent : IEvent, IRequest
{
    public EventType Type => EventType.SpaceDelete;

    public Guid SpaceId { get; set; }
}