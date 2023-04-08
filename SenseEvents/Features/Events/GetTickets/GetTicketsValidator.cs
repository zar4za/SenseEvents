using FluentValidation;

namespace SenseEvents.Features.Events.GetTickets
{
    public class GetTicketsValidator : AbstractValidator<GetTicketsQuery>
    {
        public GetTicketsValidator()
        {
            RuleFor(x => x.EventId).NotEmpty();
        }
    }
}
