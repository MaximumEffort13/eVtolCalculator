using Application.Commands.Battery;
using FluentValidation;

namespace Application.Validators.Battery;
public sealed class CreateBatteryPackCommandValidator : AbstractValidator<CreateBatteryPackCommand>
{
    public CreateBatteryPackCommandValidator()
    {
        RuleFor(b => b.Battery.CellVoltage_V).GreaterThan(0).WithMessage("Cell Voltage cannot be less than 0.");
        RuleFor(b => b.Battery.CellCurrent_mA).GreaterThan(0).WithMessage("Cell Current cannot be less than 0.");
        RuleFor(b => b.Battery.CellCapacity_mWh).GreaterThan(0).WithMessage("Cell Capacity cannot be less than 0.");
        RuleFor(b => b.Battery.CellWeight_g).GreaterThan(0).WithMessage("Cell Weight cannot be less than 0.");
        RuleFor(b => b.Battery.MiscellaneousPackWeight_kg).GreaterThan(0).WithMessage("Weight cannot be less than 0.");
    }
}
