using FluentValidation;
using JetBrains.Annotations;

namespace SenseEvents.Features.Events.AddTicket
{
    [UsedImplicitly] //used in middleware
    public class AddTicketValidator : AbstractValidator<AddTicketCommand>
    {
        public AddTicketValidator()
        {
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.OwnerId).NotEmpty();
        }
    }
}
