using Application.Commands.DesignConstants;
using Domain.Abstractions;
using FluentValidation;

namespace Application.Validators.DesignConstants;
public sealed class CreateDesignConstantsCommandValidator : AbstractValidator<CreateDesignConstantsCommand>
{
    public CreateDesignConstantsCommandValidator(IDesignConstantsRepository designConstantsRepository)
    {
        RuleFor(a => a.Name).MustAsync(async (name, _) =>
        {
            return await designConstantsRepository.IsNameUnique(name);
        }).NotEmpty().MaximumLength(50).MinimumLength(5).WithMessage("Name parameter incorrect.");

        RuleFor(m => m.Value).NotNull().NotEmpty().GreaterThan(0).WithMessage("Length rating cannot be a negative value.");
    }
}