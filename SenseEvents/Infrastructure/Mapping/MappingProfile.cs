using AutoMapper;
using SenseEvents.Features.Events;
using SenseEvents.Features.Events.AddEvent;

namespace SenseEvents.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddEventCommand, Event>()
                .ForMember(
                    destinationMember: e => e.Tickets,
                    memberOptions: o => o.MapFrom(
                        mapExpression: _ => new List<Ticket>()));
        }
    }
}
