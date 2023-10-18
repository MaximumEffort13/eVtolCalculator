using Application.MotorFacilitators.Commands;
using FluentValidation;

namespace Application.MotorFacilitators.Validators;
public sealed class CreateMotorCommandValidator : AbstractValidator<CreateMotorCommand>
{
    public CreateMotorCommandValidator()
    {
        RuleFor(a => a.Name).NotEmpty().MaximumLength(50).MinimumLength(5).WithMessage("Name parameter incorrect.");
        RuleFor(m => m.Weight_kg).NotEmpty().NotNull().GreaterThan(0).WithMessage("Weight cannot be negative");
        RuleFor(m => m.VoltageRating_V).NotEmpty().NotNull().GreaterThan(0).WithMessage("Voltage rating cannot be a negative value.");
        RuleFor(m => m.CurrentRating_A).NotNull().NotEmpty().GreaterThan(0).WithMessage("Current rating cannot be a negative value.");
        RuleFor(m => m.Kv).NotNull().NotEmpty().GreaterThan(0).WithMessage($"{nameof(CreateMotorCommand.Kv)} cannot be negative.");
    }
}
