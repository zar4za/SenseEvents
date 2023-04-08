using FluentValidation;

namespace SenseEvents.Features.Events.DeleteEvent
{
    public class DeleteEventValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventValidator(IEventsService events)
        {
            RuleFor(e => e.Id)
                .NotEmpty();
        }
    }
}
