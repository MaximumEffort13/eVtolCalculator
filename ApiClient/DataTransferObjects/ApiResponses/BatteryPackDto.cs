namespace ApiClient.DataTransferObjects.ApiResponses;

public class BatteryPackDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Capacity { get; set; }
    public string Voltage { get; set; }
    public string Current { get; set; }
    public string Weight { get; set; }
    public string SpecificEnergy { get; set; }
}
