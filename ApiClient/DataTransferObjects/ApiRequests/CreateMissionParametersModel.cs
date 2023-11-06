using System.ComponentModel.DataAnnotations;

namespace ApiClient.DataTransferObjects.ApiRequests;

public class CreateMissionParametersModel
{
    [Required]
    public double TotalStructureWeight_kg { get; set; }

    [Required]
    public double PayloadWeight_kg { get; set; }

    [Required]
    public int FlightTimeInMinutes { get; set; }
}
