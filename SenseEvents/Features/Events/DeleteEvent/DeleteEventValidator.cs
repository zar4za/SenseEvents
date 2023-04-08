using FluentValidation;

namespace SenseEvents.Features.Events.DeleteEvent
{
    public class DeleteEventValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty();
        }
    }
}
