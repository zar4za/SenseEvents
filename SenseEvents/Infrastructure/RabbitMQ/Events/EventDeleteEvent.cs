namespace SenseEvents.Infrastructure.RabbitMQ.Events;

public class EventDeleteEvent
{
    public Guid DeletedEventId { get; set; }
}