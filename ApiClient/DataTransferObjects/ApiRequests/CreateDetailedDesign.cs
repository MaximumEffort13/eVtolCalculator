using System.ComponentModel.DataAnnotations;

namespace ApiClient.DataTransferObjects.ApiRequests;

public class CreateDetailedDesign
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string BatteryPackId { get; set; }

    [Required]
    public string InverterId { get; set; }

    [Required]
    public string MotorId { get; set; }

    [Required]
    public string BladeId { get; set; }

    [Required]
    public CreateFueslageModel Fuselage { get; set; }

    [Required]
    public CreateMissionParametersModel MissionParameters { get; set; }

    [Required]
    public int MotorQuantity { get; set; }

    [Required]
    public int BladePerMotorQuantity { get; set; }

}
