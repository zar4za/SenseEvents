using FluentValidation;

namespace SenseEvents.Features.Events.DeleteEvent
{
    public class DeleteEventValidator : AbstractValidator<DeleteEventCommand>
    {
        public DeleteEventValidator(IEventsService events)
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                .Must(e => events.GetEvent(e).Result is not null)
                .WithMessage(e => $"Мероприятия с Id '{e}' не существует.");
        }
    }
}
