using SenseEvents.Infrastructure.Messaging;

namespace SenseEvents.Features.Events.DeleteEvent
{
    public class DeleteEventCommand : ICommand<DeleteEventResponse>
    {
        public Guid Id { get; set; }
    }
}
