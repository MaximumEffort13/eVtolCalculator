namespace ApiClient.DataTransferObjects.ApiResponses;

public class InverterDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string VoltageRating { get; set; }
    public string CurrentRating { get; set; }
    public string Weight { get; set; }
    public string PowerToWeightRatio { get; set; }
}
