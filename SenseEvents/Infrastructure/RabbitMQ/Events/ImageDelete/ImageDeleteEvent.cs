using MediatR;

namespace SenseEvents.Infrastructure.RabbitMQ.Events.ImageDelete;

public class ImageDeleteEvent : IEvent, IRequest
{
    public EventType Type => EventType.ImageDelete;

    public Guid ImageId { get; set; }
}