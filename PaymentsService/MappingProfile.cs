using AutoMapper;
using PaymentsService.AddPayment;

namespace PaymentsService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddPaymentCommand, Payment>()
                .ForMember(
                    destinationMember: dest => dest.Id,
                    memberOptions: opts => opts.MapFrom(
                        mapExpression: _ => Guid.NewGuid()))
                .ForMember(
                    destinationMember: dest => dest.DateCreation,
                    memberOptions: opts => opts.MapFrom(
                        mapExpression: _ => DateTimeOffset.Now))
                .ForMember(
                    destinationMember: dest => dest.State,
                    memberOptions: opts => opts.MapFrom(
                        mapExpression: _ => PaymentState.Hold));
        }
    }
}
