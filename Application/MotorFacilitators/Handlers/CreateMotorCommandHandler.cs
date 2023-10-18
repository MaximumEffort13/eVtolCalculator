using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.MotorFacilitators.Commands;
using Domain.Abstractions;
using Domain.Entities.DetailedDesign;
using Domain.Enums;
using Domain.Primitives;
using FluentResults;

namespace Application.MotorFacilitators.Handlers;

internal class CreateMotorCommandHandler : ICommandHandler<CreateMotorCommand, MotorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMotorRepository _motorRepo;

    public CreateMotorCommandHandler(IUnitOfWork unitOfWork, IMotorRepository motorRepo)
    {
        _unitOfWork = unitOfWork;
        _motorRepo = motorRepo;
    }

    public async Task<Result<MotorDto>> Handle(CreateMotorCommand request, CancellationToken cancellationToken)
    {
        MeasureandQuantity weight = new(request.Weight_kg,$"{SiPrefixes.Kilo.Name}{SiUnits.Mass.Name}");
        MeasureandQuantity kv = new(request.Kv, $"{SiUnits.Rpm.Name}/{SiUnits.Voltage.Name}");
        MeasureandQuantity currentRating = new(request.CurrentRating_A, SiUnits.Current.Name);
        MeasureandQuantity voltageRating = new(request.VoltageRating_V, SiUnits.Voltage.Name);

        var motor = new Motor(Guid.NewGuid(), request.Name, voltageRating, currentRating, weight, kv);

        _motorRepo.Create(motor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = MotorDtoMapper.Map(motor);
        return response;
    }
}
