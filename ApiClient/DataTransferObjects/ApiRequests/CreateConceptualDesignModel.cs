using System.ComponentModel.DataAnnotations;

namespace ApiClient.DataTransferObjects.ApiRequests;

public class CreateConceptualDesignModel
{
    [Required]
    public double TotalWeightOfeVtol { get; set; }

    [Required]
    public double PayloadMass { get; set; }

    [Required]
    public int FlightTimeInMinutes { get; set; }
}
