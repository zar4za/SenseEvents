using MediatR;

namespace SenseEvents.Features.Events.GetEvents
{
    public class GetEventsQuery : IRequest<GetEventsResponse>
    {
        public GetEventsQuery() { }
    }
}
