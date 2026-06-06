using Application.Abstractions;
using Application.Commands.Mission;
using Application.DTO;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities.ConceptDesign;
using Domain.Enums;
using Domain.Primitives;
using FluentResults;

namespace Application.Handlers.Mission;
internal class CreateMissionParameterCommandHandler : ICommandHandler<CreateMissionParameterCommand, MissionParameterDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMissionParameterRepository _missionParameterRepository;

    public CreateMissionParameterCommandHandler(IUnitOfWork unitOfWork, IMissionParameterRepository missionParameterRepository)
    {
        _unitOfWork = unitOfWork;
        _missionParameterRepository = missionParameterRepository;
    }

    public async Task<Result<MissionParameterDto>> Handle(CreateMissionParameterCommand request, CancellationToken cancellationToken)
    {
        var totalWeight = new MeasureandQuantity(request.Mission.TotalStructureWeight_kg, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
        var payloadWeight = new MeasureandQuantity(request.Mission.PayloadWeight_kg, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
        var flightTime = TimeSpan.FromMinutes(request.Mission.FlightTimeInMinutes);

        var missionPrameter = new MissionParameterEstimates(Guid.NewGuid(), request.UserId, totalWeight, payloadWeight, flightTime);

        _missionParameterRepository.Create(missionPrameter);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = MissionParameterDtoMapper.Map(missionPrameter);

        return response;
    }
}
