using FluentValidation;

namespace SenseEvents.Features.Events.UpdateEvent
{
    public class UpdateEventValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventValidator(IEventsService events)
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                .Must(e => events.GetEvent(e).Result is not null )
                .WithMessage(e => $"Мероприятия с Id '{e}' не существует.");
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
