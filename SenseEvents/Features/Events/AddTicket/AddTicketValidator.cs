using FluentValidation;

namespace SenseEvents.Features.Events.AddTicket
{
    public class AddTicketValidator : AbstractValidator<AddTicketCommand>
    {
        public AddTicketValidator()
        {
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.OwnerId).NotEmpty();
        }
    }
}
