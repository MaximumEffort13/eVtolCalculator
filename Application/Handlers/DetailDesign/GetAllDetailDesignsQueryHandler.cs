using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.DetailDesign;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.DetailDesign;

internal sealed class GetAllDetailDesignsQueryHandler : IQueryHandler<GetAllDetailDesignsQuery, List<ElectricVtolDesignDto>>
{
    private readonly IElectricVtolRepository _electricVtolRepository;
    private readonly IBladeRepository _bladeRepository;
    private readonly IMissionParameterRepository _missionParameterRepository;
    private readonly IBatteryPackRepository _batteryPackRepository;
    private readonly IInvererRepository _invererRepository;
    private readonly IMotorRepository _motorRepository;
    private readonly IFuselageRepository _fuselageRepository;

    public GetAllDetailDesignsQueryHandler(IElectricVtolRepository electricVtolRepository,
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

    public async Task<Result<List<ElectricVtolDesignDto>>> Handle(GetAllDetailDesignsQuery request, CancellationToken cancellationToken)
    {
        var electricVtolDesigns = await _electricVtolRepository.GetAllAsync(request.UserId, cancellationToken);

        List<ElectricVtolDesignDto> electricVtolDesignDtos = new();

        foreach (var design in electricVtolDesigns)
        {
            var battery = await _batteryPackRepository.GetByIdAsync(design.BatteryPackId, request.UserId, cancellationToken);
            var motor = await _motorRepository.GetByIdAsync(design.MotorId, request.UserId, cancellationToken);
            var inverter = await _invererRepository.GetByIdAsync(design.InverterId, request.UserId, cancellationToken);
            var blade = await _bladeRepository.GetByIdAsync(design.BladeId, request.UserId, cancellationToken);
            var fuselage = await _fuselageRepository.GetByIdAsync(design.FuselageId, request.UserId, cancellationToken);
            var mission = await _missionParameterRepository.GetByIdAsync(design.MissionParameterId, request.UserId, cancellationToken);

            var response = ElectricVtolDesignDtoMapper.Map(design);

            response.Battery = BatteryPackDtoMapper.Map(battery);
            response.Motor = MotorDtoMapper.Map(motor);
            response.Inverter = InverterDtoMapper.Map(inverter);
            response.Blade = BladeDtoMapper.Map(blade);
            response.MissionEstimates = MissionParameterDtoMapper.Map(mission);
            response.Fuselage = FuselageDtoMapper.Map(fuselage);

            electricVtolDesignDtos.Add(response);
        }

        return electricVtolDesignDtos;
    }
}
