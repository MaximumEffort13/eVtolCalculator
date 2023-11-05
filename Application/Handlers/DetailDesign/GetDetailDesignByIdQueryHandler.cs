using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.DetailDesign;
using Domain.Abstractions;
using FluentResults;
using Infrastructure.Repositories;

namespace Application.Handlers.DetailDesign;
internal class GetDetailDesignByIdQueryHandler : IQueryHandler<GetDetailDesignByIdQuery, ElectricVtolDesignDto>
{
    private readonly IElectricVtolRepository _electricVtolRepository;
    private readonly IBladeRepository _bladeRepository;
    private readonly IMissionParameterRepository _missionParameterRepository;
    private readonly IBatteryPackRepository _batteryPackRepository;
    private readonly IInvererRepository _invererRepository;
    private readonly IMotorRepository _motorRepository;
    private readonly IFuselageRepository _fuselageRepository;

    public GetDetailDesignByIdQueryHandler(IElectricVtolRepository electricVtolRepository,
        IBladeRepository bladeRepository,
        IMissionParameterRepository missionParameterRepository,
        IBatteryPackRepository batteryPackRepository,
        IInvererRepository invererRepository,
        IMotorRepository motorRepository,
        IFuselageRepository fuselageRepository)
    {
        _electricVtolRepository = electricVtolRepository;
        _bladeRepository = bladeRepository;
        _missionParameterRepository = missionParameterRepository;
        _batteryPackRepository = batteryPackRepository;
        _invererRepository = invererRepository;
        _motorRepository = motorRepository;
        _fuselageRepository = fuselageRepository;
    }

    public async Task<Result<ElectricVtolDesignDto>> Handle(GetDetailDesignByIdQuery request, CancellationToken cancellationToken)
    {
        var electricVtolDesign = await _electricVtolRepository.GetByIdAsync(request.Id, cancellationToken);

        var response = ElectricVtolDesignDtoMapper.Map(electricVtolDesign);

        var battery = await _batteryPackRepository.GetByIdAsync(electricVtolDesign.BatteryPackId, cancellationToken);
        var motor = await _motorRepository.GetByIdAsync(electricVtolDesign.MotorId, cancellationToken);
        var inverter = await _invererRepository.GetByIdAsync(electricVtolDesign.InverterId, cancellationToken);
        var blade = await _bladeRepository.GetByIdAsync(electricVtolDesign.BladeId, cancellationToken);
        var fuselage = await _fuselageRepository.GetByIdAsync(electricVtolDesign.FuselageId, cancellationToken);
        var mission = await _missionParameterRepository.GetByIdAsync(electricVtolDesign.MissionParameterId, cancellationToken);

        response.Battery = BatteryPackDtoMapper.Map(battery);
        response.Motor = MotorDtoMapper.Map(motor);
        response.Inverter = InverterDtoMapper.Map(inverter);
        response.Blade = BladeDtoMapper.Map(blade);
        response.MissionEstimates = MissionParameterDtoMapper.Map(mission);
        response.Fuselage = FuselageDtoMapper.Map(fuselage);

        return response;
    }
}
