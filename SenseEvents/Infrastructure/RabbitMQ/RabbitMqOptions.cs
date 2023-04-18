namespace SenseEvents.Infrastructure.RabbitMQ;

public class RabbitMqOptions
{
    public const string ConfigSection = "RabbitMQ";

    public string Host { get; set; } = null!;

    public string Queue { get; set; } = null!;
}