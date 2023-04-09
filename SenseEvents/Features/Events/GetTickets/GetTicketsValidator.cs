using FluentValidation;
using JetBrains.Annotations;

namespace SenseEvents.Features.Events.GetTickets
{
    [UsedImplicitly] // Used in middleware
    public class GetTicketsValidator : AbstractValidator<GetTicketsQuery>
    {
        public GetTicketsValidator()
        {
            RuleFor(x => x.EventId).NotEmpty();
        }
    }
}
