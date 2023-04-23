namespace SenseEvents.Infrastructure.RabbitMQ.Events.SpaceDelete;

public class SpaceDeleteEvent
{
    public Guid SpaceId { get; set; }
}