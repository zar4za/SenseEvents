using FluentValidation;

namespace SenseEvents.Features.Events.AddEvent
{
    public class AddEventValidator : AbstractValidator<AddEventCommand>
    {
        public AddEventValidator()
        {
            RuleFor(e => e.StartUtc).NotEmpty();
            RuleFor(e => e.EndUtc)
                .NotEmpty()
                .GreaterThan(e => e.StartUtc);

            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(e => e.Description).MaximumLength(1024);
            RuleFor(e => e.ImageId).NotEmpty();
            RuleFor(e => e.SpaceId).NotEmpty();
        }
    }
}
