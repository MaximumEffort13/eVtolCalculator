using System.ComponentModel.DataAnnotations;

namespace ApiClient.DataTransferObjects.ApiRequests;

public class CreateBladeModel
{
    [Required]
    public string BladeName { get; set; }

    [Required]
    public double Length_mm { get; set; }

    [Required]
    public double Width_mm { get; set; }

    [Required]
    public double Thickness_mm { get; set; }

    [Required]
    public double Weight_g { get; set; }

    [Required]
    public double AngelOfAttack { get; set; }
}
