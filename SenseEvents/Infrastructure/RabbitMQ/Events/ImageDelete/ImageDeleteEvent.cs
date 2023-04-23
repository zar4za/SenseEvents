namespace SenseEvents.Infrastructure.RabbitMQ.Events.ImageDelete;

public class ImageDeleteEvent
{
    public Guid ImageId { get; set; }
}