using FluentValidation;
using JetBrains.Annotations;

namespace SenseEvents.Features.Tickets.GetTickets;

[UsedImplicitly] // Used in middleware
public class GetTicketsValidator : AbstractValidator<GetTicketsQuery>
{
    public GetTicketsValidator()
    {
        RuleFor(x => x.EventId).NotEmpty();
    }
}