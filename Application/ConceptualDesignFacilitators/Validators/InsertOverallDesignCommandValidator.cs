using Application.ConceptualDesignFacilitators.Commands;
using FluentValidation;
using Domain.Abstractions;

namespace Application.ConceptualDesignFacilitators.Validators;

public sealed class InsertOverallDesignCommandValidator : AbstractValidator<CreateConceptualDesignCommand>
{
    public InsertOverallDesignCommandValidator(IConceptualDesignRepository designRepository)
    {
        RuleFor(d => d.TotalMassOfeVtol_kg).Must((value) =>
        {
            return value > 0;
        }).WithMessage("Mass should always be a positive value");

        RuleFor(d => d.PayloadMass_kg).NotNull().Must((value) =>
        {
            return value > 0;
        }).WithMessage("Mass must be larger than 0");

        RuleFor(d => d.FlightTimeInMinutes).NotNull().Must((value) =>
        {
            return value > 0;
        }).WithMessage("Flight time should be more longer than 0.");

    }
}
