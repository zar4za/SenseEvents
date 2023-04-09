using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.GetEvents
{
    [UsedImplicitly] // Mediator
    public class GetEventsQuery : IRequest<GetEventsResponse>
    {
    }
}
