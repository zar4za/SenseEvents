using FluentValidation;
using JetBrains.Annotations;
using SenseEvents.Infrastructure.Identity;
using SenseEvents.Infrastructure.Services;
using SenseEvents.Infrastructure.Services.Images;

namespace SenseEvents.Features.Events.AddEvent;

[UsedImplicitly] //used by middleware
public class AddEventValidator : AbstractValidator<AddEventCommand>
{
    public AddEventValidator(IImageService images, ISpaceService spaces)
    {
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