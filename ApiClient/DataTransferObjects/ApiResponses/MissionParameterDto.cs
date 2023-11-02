namespace ApiClient.DataTransferObjects.ApiResponses;

public class MissionParameterDto
{
    public Guid Id { get; set; }
    public string EstimatedPowerRequirement { get; set; }
    public string EstimatedBatteryCapacityRequirement { get; set; }
    public string EstimatedBatteryWeight { get; set; }
    public string EstimatedMotorWeight { get; set; }
    public string EstimatedHorsepowerRequiredForHover { get; set; }
}
