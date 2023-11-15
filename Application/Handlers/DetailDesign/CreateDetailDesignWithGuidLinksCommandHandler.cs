using Application.Abstractions;
using Application.Commands.DetailDesign;
using Application.DTO;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities.DetailedDesign;
using Domain.EntityCalculations;
using FluentResults;

namespace Application.Handlers.DetailDesign;

internal class CreateDetailDesignWithGuidLinksCommandHandler : ICommandHandler<CreateDetailDesignWithGuidLinksCommand, ElectricVtolDesignDto>
{
    private readonly IElectricVtolRepository _electricVtolRepository;
    private readonly IBladeRepository _bladeRepository;
    private readonly IMissionParameterRepository _missionParameterRepository;
    private readonly IBatteryPackRepository _batteryPackRepository;
    private readonly IInvererRepository _invererRepository;
    private readonly IMotorRepository _motorRepository;
    private readonly IFuselageRepository _fuselageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDetailDesignWithGuidLinksCommandHandler(
        IElectricVtolRepository electricVtolRepository,
        IBladeRepository bladeRepository,
        IMissionParameterRepository missionParameterRepository,
        IBatteryPackRepository batteryPackRepository,
        IInvererRepository invererRepository,
        IMotorRepository motorRepository,
        IFuselageRepository fuselageRepository,
        IUnitOfWork unitOfWork)
    {
        _electricVtolRepository = electricVtolRepository;
        _bladeRepository = bladeRepository;
        _missionParameterRepository = missionParameterRepository;
        _batteryPackRepository = batteryPackRepository;
        _invererRepository = invererRepository;
        _motorRepository = motorRepository;
        _fuselageRepository = fuselageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ElectricVtolDesignDto>> Handle(CreateDetailDesignWithGuidLinksCommand request, CancellationToken cancellationToken)
    {
        var electricVtol = new ElectricVtolDesign(
            Guid.NewGuid(),
            request.UserId,
            request.Name,
            request.BatteryId,
            request.InverterId,
            request.MotorId,
            request.BladeId,
            request.FuselageId,
            request.MissionId,
            request.MotorQuantity,
            request.BladePerMotorQuantity);

        var battery = await _batteryPackRepository.GetByIdAsync(request.BatteryId, request.UserId, cancellationToken);
        var motor = await _motorRepository.GetByIdAsync(request.MotorId, request.UserId, cancellationToken);
        var inverter = await _invererRepository.GetByIdAsync(request.InverterId, request.UserId, cancellationToken);
        var blade = await _bladeRepository.GetByIdAsync(request.BladeId, request.UserId, cancellationToken);
        var fuselage = await _fuselageRepository.GetByIdAsync(request.FuselageId, request.UserId, cancellationToken);
        var mission = await _missionParameterRepository.GetByIdAsync(request.MissionId, request.UserId, cancellationToken);

        electricVtol.ThrustArea = AerodynamicCalculations.CalculateThrustArea(blade.Length, request.MotorQuantity, request.BladePerMotorQuantity);
        electricVtol.Thrust = AerodynamicCalculations.CalculateThrustRequirement(mission.EstimatedPowerRequirement, electricVtol.ThrustArea);
        electricVtol.FlightTimeInMinutes = mission.FlightTimeRequirementInMinutes;

        electricVtol.LiftOffWeight = MechanicalCalculations.CalculateLiftOffWeight(
            mission.PayloadWeight,
            battery.Mass,
            motor.Weight,
            inverter.Weight,
            blade.Weight,
            fuselage.Weight,
            request.MotorQuantity,
            request.BladePerMotorQuantity);

        electricVtol.DiscLoading = AerodynamicCalculations.CalculateDiscLoading(electricVtol.LiftOffWeight, electricVtol.ThrustArea);
        electricVtol.PowerLoading = AerodynamicCalculations.CalculatePowerLoading(electricVtol.LiftOffWeight, mission.EstimatedHorsepowerRequiredForHover);
        electricVtol.PayloadWeight = mission.PayloadWeight;

        _electricVtolRepository.Create(electricVtol);
        await _unitOfWork.SaveChangesAsync();

        var response = ElectricVtolDesignDtoMapper.Map(electricVtol);
        response.Battery = BatteryPackDtoMapper.Map(battery);
        response.Motor = MotorDtoMapper.Map(motor);
        response.Inverter = InverterDtoMapper.Map(inverter);
        response.Blade = BladeDtoMapper.Map(blade);
        response.MissionEstimates = MissionParameterDtoMapper.Map(mission);
        response.Fuselage = FuselageDtoMapper.Map(fuselage);


        return response;
    }
}
