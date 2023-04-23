using AutoMapper;
using JetBrains.Annotations;
using MassTransit;
using MediatR;
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
            await _bus.Publish(_mapper.Map<EventDeleteEvent>(request), cancellationToken);
        }

        return new DeleteEventResponse
        {
            Success = success
        };
    }
}