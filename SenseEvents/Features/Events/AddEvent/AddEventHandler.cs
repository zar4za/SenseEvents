using AutoMapper;
using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.AddEvent;

[UsedImplicitly] //mediator
public class AddEventHandler : IRequestHandler<AddEventCommand, AddEventResponse>
{
    private readonly IEventsService _eventsService;
    private readonly IMapper _mapper;

    public AddEventHandler(IEventsService eventsService, IMapper mapper)
    {
        _eventsService = eventsService;
        _mapper = mapper;
    }

    public async Task<AddEventResponse> Handle(AddEventCommand command, CancellationToken cancellationToken)
    {
        var newEvent = _mapper.Map<Event>(command);
        var id = await _eventsService.AddEvent(newEvent);

        return new AddEventResponse
        {
            Id = id
        };
    }
}