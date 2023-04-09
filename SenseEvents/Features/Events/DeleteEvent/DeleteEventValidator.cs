using FluentValidation;
using JetBrains.Annotations;

namespace SenseEvents.Features.Events.DeleteEvent
{
    [UsedImplicitly] // Mediator
    public class DeleteEventValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty();
        }
    }
}
