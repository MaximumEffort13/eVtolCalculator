using Application.Commands.Motors;
using FluentValidation;

namespace Application.Validators.Motors;
public sealed class CreateMotorCommandValidator : AbstractValidator<CreateMotorCommand>
{
    public CreateMotorCommandValidator()
    {
        RuleFor(a => a.Motor.Name).NotEmpty().MaximumLength(50).MinimumLength(5).WithMessage("Name parameter incorrect.");
        RuleFor(m => m.Motor.Weight_kg).NotEmpty().NotNull().GreaterThan(0).WithMessage("Weight cannot be negative");
        RuleFor(m => m.Motor.VoltageRating_V).NotEmpty().NotNull().GreaterThan(0).WithMessage("Voltage rating cannot be a negative value.");
        RuleFor(m => m.Motor.CurrentRating_A).NotNull().NotEmpty().GreaterThan(0).WithMessage("Current rating cannot be a negative value.");
        RuleFor(m => m.Motor.Kv).NotNull().NotEmpty().GreaterThan(0).WithMessage($"{nameof(CreateMotorCommand.Motor.Kv)} cannot be negative.");
    }
}
