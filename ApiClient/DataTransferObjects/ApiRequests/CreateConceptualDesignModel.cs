using System.ComponentModel.DataAnnotations;

namespace ApiClient.DataTransferObjects.ApiRequests;

public class CreateConceptualDesignModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public double TotalMassOfeVtol_kg { get; set; }

    [Required]
    public double PayloadMass_kg { get; set; }

    [Required]
    public int FlightTimeInMinutes { get; set; }
}
