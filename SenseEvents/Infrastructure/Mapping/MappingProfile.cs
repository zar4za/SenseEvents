using AutoMapper;
using FluentValidation;
using SC.Internship.Common.ScResult;
using SenseEvents.Features.Events;
using SenseEvents.Features.Events.AddEvent;
using SenseEvents.Features.Events.DeleteEvent;
using SenseEvents.Features.Events.UpdateEvent;
using SenseEvents.Infrastructure.RabbitMQ.Events;
using SC.Internship.Common.Exceptions;
using SenseEvents.Features.Tickets;

namespace SenseEvents.Infrastructure.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddEventCommand, Event>()
            .ForMember(
                destinationMember: dest => dest.Tickets,
                memberOptions: opts => opts.MapFrom(
                    mapExpression: _ => new List<Ticket>()));

        CreateMap<UpdateEventCommand, Event>();

        CreateMap<DeleteEventCommand, EventDeleteEvent>()
            .ForMember(
                destinationMember: dest => dest.DeletedEventId,
                memberOptions: opts => opts.MapFrom(
                    mapExpression: src => src.Id));

        CreateMap<ValidationException, ScError>()
            .ForMember(
                destinationMember: dest => dest.Message,
                memberOptions: opts => opts.MapFrom(
                    mapExpression: src => src.Message))
            .ForMember(
                destinationMember: dest => dest.ModelState,
                memberOptions: opts => opts.MapFrom(
                    mapExpression: src => src.Errors.ToDictionary(
                        x => x.PropertyName,
                        x => src.Errors
                            .Where(y => x.PropertyName == y.PropertyName)
                            .Select(y => y.ErrorMessage)
                            .ToList())));

        CreateMap<ScException, ScError>();
        CreateMap<InvalidOperationException, ScError>();
    }
}