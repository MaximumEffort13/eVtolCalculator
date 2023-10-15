using Application.Abstractions;
using Application.DTO;
using Application.InverterFacilitators.Commands;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities.DetailedDesign;
using Domain.Enums;
using Domain.Primitives;
using FluentResults;

namespace Application.InverterFacilitators.Handlers;

internal class CreateInverterCommandHandler : ICommandHandler<CreateInverterCommand, InverterDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInvererRepository _invererRepository;

    public CreateInverterCommandHandler(IUnitOfWork unitOfWork, IInvererRepository invererRepository)
    {
        _unitOfWork = unitOfWork;
        _invererRepository = invererRepository;
    }

    public async Task<Result<InverterDto>> Handle(CreateInverterCommand request, CancellationToken cancellationToken)
    {
        MeasureandQuantity weight = new(request.Weight_kg,$"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}");
        MeasureandQuantity currentRating = new(request.CurrentRating, SiUnits.Current.Name);
        MeasureandQuantity voltageRating = new(request.VoltageRating, SiUnits.Voltage.Name);

        var inverter = new Inverter(Guid.NewGuid(), request.Name, weight, voltageRating, currentRating);

        _invererRepository.Create(inverter);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = InverterDtoMapper.Map(inverter);
        return response;
    }
}
