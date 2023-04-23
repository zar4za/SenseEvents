namespace SenseEvents.Infrastructure.RabbitMQ;

public class RabbitMqOptions
{
    public const string ConfigSection = "RabbitMQ";

    public string Host { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}