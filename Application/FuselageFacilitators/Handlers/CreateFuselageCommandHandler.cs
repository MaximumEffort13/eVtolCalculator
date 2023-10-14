using Application.Abstractions;
using Application.DTO;
using Application.FuselageFacilitators.Commands;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities.DetailedDesign;
using Domain.Enums;
using Domain.Primitives;
using FluentResults;

namespace Application.FuselageFacilitators.Handlers;

public sealed class CreateFuselageCommandHandler : ICommandHandler<CreateFuselageCommand, FuselageDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFuselageRepository _fuselageRepository;

    public CreateFuselageCommandHandler(IUnitOfWork unitOfWork, IFuselageRepository fuselageRepository)
    {
        _unitOfWork = unitOfWork;
        _fuselageRepository = fuselageRepository;
    }

    public string SiPrefix { get; private set; }

    public async Task<Result<FuselageDto>> Handle(CreateFuselageCommand request, CancellationToken cancellationToken)
    {
        MeasureandQuantity weight = new(request.Weight, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);

        var fuselage = new Fuselage(Guid.NewGuid(), weight);

        _fuselageRepository.Create(fuselage);
        await _unitOfWork.SaveChangesAsync();

        var response = FuselageDtoMapper.Map(fuselage);

        return response;
    }
}
