using SenseEvents.Infrastructure.RabbitMQ.Events;

namespace SenseEvents.Infrastructure.RabbitMQ;

public interface IBus
{
    Task SendAsync<T>(T message) where T : IEvent;
}