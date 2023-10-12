using Domain.Primitives;

namespace Application.DTO;

public sealed class ElectricVtolDesignDto
{
    public BatteryPackDto Battery { get; set; }
    public MotorDto Motor { get; set; }
    public InverterDto Invert { get; set; }
    public FuselageDto Fuselage { get; set; }
    public MissionParameterDto MissionEstimates { get; set; }

    public string LiftOffWeight { get; set; }
    public string PayloadWeight { get; set; }
    public double FlightTimeInMinutes { get; set; }
    public string DiscLoading { get; set; }
    public string PowerLoading { get; set; }
    public string Thrust { get; set; }
}
