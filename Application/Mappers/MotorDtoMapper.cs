using Application.DTO;
using Domain.Entities.DetailedDesign;

namespace Application.Mappers;

public static class MotorDtoMapper
{
    public static MotorDto Map(Motor motor)
    {
        return new MotorDto
        { 
            Id = motor.Id.ToString(),
            Name = motor.Name,
            VoltageRating = $"{motor.VoltageRating.Value} {motor.VoltageRating.Unit}",
            CurrentRating = $"{motor.CurrentRating.Value} {motor.CurrentRating.Unit}",
            Weight = $"{motor.Weight.Value} {motor.Weight.Unit}",
            PowerToWeightRatio = $"{motor.PowerToWeightRatio.Value} {motor.PowerToWeightRatio.Unit}",
            RpmValue  = $"{motor.Rpm.Value} {motor.Rpm.Unit}",
            Torque = $"{motor.Torque.Value} {motor.Torque.Unit}",
            Efficiency = $"{motor.Efficiency * 100}%"
        };
    }
}
