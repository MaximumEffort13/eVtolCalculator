using Application.DetailDesignFacilitators.Commands;
using FluentValidation;

namespace Application.DetailDesignFacilitators.Validators;
public sealed class CreateDetailDesignCommandValidator : AbstractValidator<CreateDetailedDesignCommand>
{
    public CreateDetailDesignCommandValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(50)
            .MinimumLength(5)
            .WithMessage("Name is required and should be at least 5 characters long.");

        RuleFor(d => d.FlightTimeInMinutes)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Flight time cannot be less than or equal to 0.");
    }
}
