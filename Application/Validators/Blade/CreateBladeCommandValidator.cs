using Application.Commands.Blade;
using FluentValidation;

namespace Application.Validators.Blade;
internal class CreateBladeCommandValidator : AbstractValidator<CreateBladeCommand>
{
    public CreateBladeCommandValidator()
    {
        RuleFor(a => a.Name).NotEmpty().MaximumLength(50).MinimumLength(5).WithMessage("Name parameter incorrect.");
        RuleFor(m => m.Length_mm).NotNull().NotEmpty().GreaterThan(0).WithMessage("Length rating cannot be a negative value.");
        RuleFor(m => m.Widht_mm).NotEmpty().NotNull().GreaterThan(0).WithMessage("Width cannot be negative value.");
        RuleFor(m => m.Thickness_mm).NotEmpty().NotNull().GreaterThan(0).WithMessage("Thickness rating cannot be a negative value.");
        RuleFor(m => m.Weight_g).NotEmpty().NotNull().GreaterThan(0).WithMessage("Weight cannot be negative");
        RuleFor(m => m.AngleOfAttack).NotNull().NotEmpty().GreaterThan(10).LessThan(90).WithMessage("Angle of attack should be greater than 0 and less than 90.");
    }
}
