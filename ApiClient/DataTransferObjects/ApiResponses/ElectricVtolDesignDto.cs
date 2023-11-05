namespace ApiClient.DataTransferObjects.ApiResponses;

public sealed class ElectricVtolDesignDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public BatteryPackDto Battery { get; set; }
    public MotorDto Motor { get; set; }
    public BladeDto Blade { get; set; }
    public InverterDto Inverter { get; set; }
    public FuselageDto Fuselage { get; set; }
    public MissionParameterDto MissionEstimates { get; set; }

    public string LiftOffWeight { get; set; }
    public string PayloadWeight { get; set; }
    public double FlightTimeInMinutes { get; set; }
    public string DiscLoading { get; set; }
    public string PowerLoading { get; set; }
    public string Thrust { get; set; }
}
