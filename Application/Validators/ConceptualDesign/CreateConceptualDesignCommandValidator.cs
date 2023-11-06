using FluentValidation;
using Domain.Abstractions;
using Application.Commands.ConceptualDesign;

namespace Application.Validators.ConceptualDesign;

public sealed class CreateConceptualDesignCommandValidator : AbstractValidator<CreateConceptualDesignCommand>
{
    public CreateConceptualDesignCommandValidator(IConceptualDesignRepository designRepository)
    {
        RuleFor(d => d.ConceptualDesignInsert.TotalMassOfeVtol_kg).Must((value) =>
        {
            return value > 0;
        }).WithMessage("Mass should always be a positive value");

        RuleFor(d => d.ConceptualDesignInsert.PayloadMass_kg).NotNull().Must((value) =>
        {
            return value > 0;
        }).WithMessage("Mass must be larger than 0");

        RuleFor(d => d.ConceptualDesignInsert.FlightTimeInMinutes).NotNull().Must((value) =>
        {
            return value > 0;
        }).WithMessage("Flight time should be more longer than 0.");
    }
}
