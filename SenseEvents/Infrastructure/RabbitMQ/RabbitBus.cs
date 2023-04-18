using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SenseEvents.Infrastructure.RabbitMQ.Events;

namespace SenseEvents.Infrastructure.RabbitMQ;

public class RabbitBus : IBus
{
    private readonly IModel _model;
    private readonly RabbitMqOptions _options;

    public RabbitBus(IConnectionFactory factory, IOptions<RabbitMqOptions> options)
    {
        var connection = factory.CreateConnection();
        _model = connection.CreateModel();
        _options = options.Value;
    }

    public async Task SendAsync<T>(T newEvent) where T : IEvent
    {
        await Task.Run(() =>
        {
            _model.QueueDeclare(_options.Queue);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newEvent));

            _model.BasicPublish(
                exchange: string.Empty,
                routingKey: newEvent.Type.ToString(),
                body: body);
        });
    }
}