using System.Text.Json.Serialization;

namespace SenseEvents.Infrastructure.RabbitMQ.Events;

public interface IEvent
{
    [JsonIgnore]
    public EventType Type { get; }
}