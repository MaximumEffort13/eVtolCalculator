using FluentResults;
using Application.Abstractions;
using Domain.Abstractions;
using Domain.Enums;
using Application.ConceptualDesignFacilitators.Commands;
using Domain.Primitives;
using Domain.Entities.ConceptDesign;
using Application.DTO;
using Application.Mappers;

namespace Application.ConceptualDesignFacilitators.Handlers;

internal sealed class CreateConceptualDesignCommandHandler : ICommandHandler<CreateConceptualDesignCommand, ConceptualDesignDto>
{
    private readonly IConceptualDesignRepository _designRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateConceptualDesignCommandHandler(IConceptualDesignRepository designRepository, IUnitOfWork unitOfWork)
    {
        _designRepository = designRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ConceptualDesignDto>> Handle(CreateConceptualDesignCommand request, CancellationToken cancellationToken)
    {
        MeasureandQuantity designWeight = new(request.TotalMassOfeVtol, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
        MeasureandQuantity payloadWeight = new(request.PayloadMass, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
        TimeSpan flightTime = TimeSpan.FromMinutes(request.FlightTimeInMinutes);

        var design = new ConceptualVtolDesign(Guid.NewGuid(), designWeight, payloadWeight, flightTime);

        _designRepository.Insert(design);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = ConceptualDesignDtoMapper.Map(design);

        return response;
    }
}
