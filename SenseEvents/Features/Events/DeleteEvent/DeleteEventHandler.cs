using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SenseEvents.Infrastructure.RabbitMQ;
using SenseEvents.Infrastructure.RabbitMQ.Events;

namespace SenseEvents.Features.Events.DeleteEvent;

[UsedImplicitly] // Mediator
public class DeleteEventHandler : IRequestHandler<DeleteEventCommand, DeleteEventResponse>
{
    private readonly IEventsService _eventsService;
    private readonly IBus _bus;
    private readonly IMapper _mapper;

    public DeleteEventHandler(IEventsService eventsService, IBus bus, IMapper mapper)
    {
        _eventsService = eventsService;
        _bus = bus;
        _mapper = mapper;
    }

    public async Task<DeleteEventResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var success = await  _eventsService.DeleteEvent(request.Id);

        if (success)
        {
            await _bus.SendAsync(_mapper.Map<EventDeleteEvent>(request));
        }

        return new DeleteEventResponse
        {
            Success = success
        };
    }
}