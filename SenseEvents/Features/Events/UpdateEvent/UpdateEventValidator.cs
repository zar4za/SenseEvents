using FluentValidation;
using JetBrains.Annotations;
using SenseEvents.Infrastructure.Identity;

namespace SenseEvents.Features.Events.UpdateEvent;

[UsedImplicitly] // Used in middleware
public class UpdateEventValidator : AbstractValidator<UpdateEventCommand>
{
    public UpdateEventValidator(IImageService images, ISpaceService spaces)
    {
        RuleFor(e => e.Id).NotEmpty();
        RuleFor(e => e.StartUtc).NotEmpty();
        RuleFor(e => e.EndUtc)
            .NotEmpty()
            .GreaterThan(e => e.StartUtc);

        RuleFor(e => e.Name)
            .NotEmpty()
            .MaximumLength(64);

        RuleFor(e => e.Description).MaximumLength(1024);
        RuleFor(e => e.ImageId)
            .NotEmpty()
            .Must(images.ImageExists)
            .WithMessage(e => $"Изображения с Id '{e}' не существует.");

        RuleFor(e => e.SpaceId)
            .NotEmpty()
            .Must(spaces.SpaceExists)
            .WithMessage(e => $"Изображения с Id '{e}' не существует.");
    }
}