using AutoMapper;
using JetBrains.Annotations;
using MediatR;

namespace SenseEvents.Features.Events.UpdateEvent;

[UsedImplicitly] // Mediator
public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, UpdateEventResponse>
{
    private readonly IEventsService _eventsService;
    private readonly IMapper _mapper;

    public UpdateEventHandler(IEventsService eventsService, IMapper mapper)
    {
        _eventsService = eventsService;
        _mapper = mapper;
    }


    public async Task<UpdateEventResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var update = _mapper.Map<Event>(request);

        var success = await _eventsService.UpdateEvent(update);

        return new UpdateEventResponse
        {
            Success = success
        };
    }
}