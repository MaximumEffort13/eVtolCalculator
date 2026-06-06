using Application.Commands.Inverter;
using FluentValidation;

namespace Application.Validators.Inverter;
public sealed class CreateInverterCommandValidator : AbstractValidator<CreateInverterCommand>
{
    public CreateInverterCommandValidator()
    {
        RuleFor(a => a.Inverter.Name).NotEmpty().MaximumLength(50).MinimumLength(5).WithMessage("Name parameter incorrect.");
        RuleFor(m => m.Inverter.Weight_kg).NotEmpty().NotNull().GreaterThan(0).WithMessage("Weight cannot be negative");
        RuleFor(m => m.Inverter.VoltageRating_V).NotEmpty().NotNull().GreaterThan(0).WithMessage("Voltage rating cannot be a negative value.");
        RuleFor(m => m.Inverter.CurrentRating_A).NotNull().NotEmpty().GreaterThan(0).WithMessage("Current rating cannot be a negative value.");
    }
}
