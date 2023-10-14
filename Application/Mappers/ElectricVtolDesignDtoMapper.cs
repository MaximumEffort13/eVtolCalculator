using Application.DTO;
using Domain.Entities.DetailedDesign;

namespace Application.Mappers;

public static class ElectricVtolDesignDtoMapper
{
    public static ElectricVtolDesignDto Map(ElectricVtolDesign eVtolDesign)
    {
        return new ElectricVtolDesignDto
        {
            Name = eVtolDesign.Name,
            Battery = BatteryPackDtoMapper.Map(eVtolDesign.Battery),
            Invert = InverterDtoMapper.Map(eVtolDesign.Inverter),
            Motor = MotorDtoMapper.Map(eVtolDesign.Motor),
            Fuselage = FuselageDtoMapper.Map(eVtolDesign.Fuselage),
            MissionEstimates = MissionParameterDtoMapper.Map(eVtolDesign.MissionParameters),

            LiftOffWeight = $"{eVtolDesign.LiftOffWeight.Value} {eVtolDesign.LiftOffWeight.Unit}",
            FlightTimeInMinutes = eVtolDesign.FlightTimeInMinutes.TotalMinutes,
            PayloadWeight = $"{eVtolDesign.PayloadWeight.Value} {eVtolDesign.PayloadWeight.Unit}",
            DiscLoading = $"{eVtolDesign.DiscLoading.Value} {eVtolDesign.DiscLoading.Unit}",
            PowerLoading = $"{eVtolDesign.PowerLoading.Value} {eVtolDesign.PowerLoading.Unit}",
            Thrust = $"{eVtolDesign.Thrust.Value} {eVtolDesign.Thrust.Unit}",
        };
    }
}
