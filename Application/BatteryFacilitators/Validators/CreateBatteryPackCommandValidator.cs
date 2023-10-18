using Application.BatteryFacilitators.Commands;
using FluentValidation;

namespace Application.BatteryFacilitators.Validators;
public sealed class CreateBatteryPackCommandValidator : AbstractValidator<CreateBatteryPackCommand>
{
    public CreateBatteryPackCommandValidator()
    {
        RuleFor(b => b.CellVoltage_V).GreaterThan(0).WithMessage("Cell Voltage cannot be less than 0.");
        RuleFor(b => b.CellCurrent_mA).GreaterThan(0).WithMessage("Cell Current cannot be less than 0.");
        RuleFor(b => b.CellCapacity_mWh).GreaterThan(0).WithMessage("Cell Capacity cannot be less than 0.");
        RuleFor(b => b.CellWeight_g).GreaterThan(0).WithMessage("Cell Weight cannot be less than 0.");
        RuleFor(b => b.MiscellaneousPackWeight_kg).GreaterThan(0).WithMessage("Weight cannot be less than 0.");
    }
}
