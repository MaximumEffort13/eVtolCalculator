using Application.Abstractions;
using Application.DetailDesignFacilitators.Commands;
using Application.DTO;
using Application.Mappers;
using Domain.Entities.DetailedDesign;
using Domain.EntityCalculations;
using Domain.Enums;
using Domain.Primitives;
using FluentResults;
using Infrastructure.Repositories;

namespace Application.DetailDesignFacilitators.Handlers;
internal class CreateDetailDesignCommandHandler : ICommandHandler<CreateDetailedDesignCommand, ElectricVtolDesignDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IElectricVtolRepository _electricVtolRepository;

    public CreateDetailDesignCommandHandler(IUnitOfWork unitOfWork, IElectricVtolRepository electricVtolRepository)
    {
        _unitOfWork = unitOfWork;
        _electricVtolRepository = electricVtolRepository;
    }

    public async Task<Result<ElectricVtolDesignDto>> Handle(CreateDetailedDesignCommand request, CancellationToken cancellationToken)
    {
        MeasureandQuantity payloadWeight = new MeasureandQuantity(request.PayloadWeight, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);
        var bladeLength = MeasureandQuantity.ConvertStringToMeasureandQuantity(request.Blade.Length);
        var powerRequirement = MeasureandQuantity.ConvertStringToMeasureandQuantity(request.MissionParameter.EstimatedPowerRequirement);
        var motorWeight = MeasureandQuantity.ConvertStringToMeasureandQuantity(request.Motor.Weight);
        var batteryWeight = MeasureandQuantity.ConvertStringToMeasureandQuantity(request.Battery.Weight);
        var inverterWeight = MeasureandQuantity.ConvertStringToMeasureandQuantity(request.Inverter.Weight);
        var bladeWeight = MeasureandQuantity.ConvertStringToMeasureandQuantity(request.Blade.Weight);
        var fuselageWeight = MeasureandQuantity.ConvertStringToMeasureandQuantity(request.Fuselage.Weight);
        var horsepowerRequired = MeasureandQuantity.ConvertStringToMeasureandQuantity(request.MissionParameter.EstimatedHorsepowerRequiredForHover);

        var electricVtol = new ElectricVtolDesign(
            Guid.NewGuid(),
            request.Name,
            request.Battery.Id,
            request.Inverter.Id,
            request.Motor.Id,
            request.Blade.Id,
            request.Fuselage.Id,
            request.MissionParameter.Id,
            request.MotorQuantity,
            request.BladePerMotorQuantity);

        electricVtol.ThrustArea = AerodynamicCalculations.CalculateThrustArea(bladeLength, request.MotorQuantity);
        electricVtol.Thrust = AerodynamicCalculations.CalculateThrustRequirement(powerRequirement, electricVtol.ThrustArea);

        electricVtol.LiftOffWeight = MechanicalCalculations.CalculateLiftOffWeight(
            payloadWeight,
            batteryWeight,
            motorWeight,
            inverterWeight,
            bladeWeight,
            fuselageWeight,
            request.MotorQuantity,
            request.BladePerMotorQuantity);

        electricVtol.DiscLoading = AerodynamicCalculations.CalculateDiscLoading(electricVtol.LiftOffWeight, electricVtol.ThrustArea);
        electricVtol.PowerLoading = AerodynamicCalculations.CalculatePowerLoading(electricVtol.LiftOffWeight, horsepowerRequired);


        _electricVtolRepository.Create(electricVtol);
        await _unitOfWork.SaveChangesAsync();

        var response = ElectricVtolDesignDtoMapper.Map(electricVtol);

        return response;
    }
}
