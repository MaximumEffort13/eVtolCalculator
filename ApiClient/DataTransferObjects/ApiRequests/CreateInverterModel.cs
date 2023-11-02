using System.ComponentModel.DataAnnotations;

namespace ApiClient.DataTransferObjects.ApiRequests;

public class CreateInverterModel
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public double VoltageRating_V { get; set; }

    [Required]
    public double CurrentRating_A { get; set; }

    [Required]
    public double Weight { get; set; }
}
