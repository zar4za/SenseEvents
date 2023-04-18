using AutoMapper;
using SenseEvents.Features.Events;
using SenseEvents.Features.Events.AddEvent;
using SenseEvents.Features.Events.DeleteEvent;
using SenseEvents.Features.Events.UpdateEvent;
using SenseEvents.Infrastructure.RabbitMQ.Events;

namespace SenseEvents.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddEventCommand, Event>()
            .ForMember(
                destinationMember: dest => dest.Tickets,
                memberOptions: o => o.MapFrom(
                    mapExpression: _ => new List<Ticket>()));

        CreateMap<UpdateEventCommand, Event>();

        CreateMap<DeleteEventCommand, EventDeleteEvent>()
            .ForMember(
                destinationMember: dest => dest.DeletedEventId,
                memberOptions: o => o.MapFrom(
                    mapExpression: src => src.Id));
    }
}