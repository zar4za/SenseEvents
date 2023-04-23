using MassTransit;
using SenseEvents.Features.Events;

namespace SenseEvents.Infrastructure.RabbitMQ.Events.ImageDelete
{
    public class ImageDeleteConsumer : IConsumer<ImageDeleteEvent>
    {
        private readonly IEventsService _eventsService;

        public ImageDeleteConsumer(IEventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public async Task Consume(ConsumeContext<ImageDeleteEvent> context)
        {
            await _eventsService.UpdateImage(context.Message.ImageId, null);
        }
    }
}
