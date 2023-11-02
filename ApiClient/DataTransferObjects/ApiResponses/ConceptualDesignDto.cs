namespace ApiClient.DataTransferObjects.ApiResponses;
public sealed class ConceptualDesignDto
{
    public Guid Id { get; set; }
    public string ProwerRequirement { get; set; }
    public string BatteryCapacityRequirement { get; set; }
    public string BatteryWeight { get; set; }
    public string MotorWeight { get; set; }
    public string Horsepower { get; set; }
}
