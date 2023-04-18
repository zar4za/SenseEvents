namespace SenseEvents.Infrastructure.RabbitMQ.Events;

public class EventDeleteEvent : IEvent
{
    public EventType Type => EventType.EventDelete;
    public Guid DeletedEventId { get; set; }
}