using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SenseEvents.Infrastructure.RabbitMQ.Events;
using SenseEvents.Infrastructure.RabbitMQ.Events.ImageDelete;
using SenseEvents.Infrastructure.RabbitMQ.Events.SpaceDelete;

namespace SenseEvents.Infrastructure.RabbitMQ;

public class RabbitMqListener : BackgroundService
{
    private readonly IModel _channel;
    private readonly IMediator _mediator;
    private readonly RabbitMqOptions _options;

    public RabbitMqListener(IConnectionFactory factory, IMediator mediator, IOptions<RabbitMqOptions> options)
    {
        _mediator = mediator;
        _options = options.Value;
        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _channel.QueueDeclare(
            queue: _options.Queue, 
            durable: false, 
            exclusive: false, 
            autoDelete: false, 
            arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (_, args) =>
        {
            var type = Enum.Parse<EventType>(args.BasicProperties.Type);
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            IEvent newEvent = type switch
            {
                EventType.ImageDelete => JsonSerializer.Deserialize<ImageDeleteEvent>(args.Body.Span)!,
                EventType.SpaceDelete => JsonSerializer.Deserialize<SpaceDeleteEvent>(args.Body.Span)!,
                _ => throw new ArgumentException("Unknown event type")
            };

            _mediator.Send(newEvent, stoppingToken);
            _channel.BasicAck(args.DeliveryTag, false);
        };

        _channel.BasicConsume(_options.Queue, true, consumer);

        return Task.CompletedTask;
    }
    
    public override void Dispose()
    {
        _channel.Close();
        base.Dispose();
    }
}