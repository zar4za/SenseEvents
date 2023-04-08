using MediatR;

namespace SenseEvents.Features.Events.DeleteEvent
{
    public class DeleteEventCommand : IRequest<DeleteEventResponse>
    {
        public Guid Id { get; init; }
    }
}
