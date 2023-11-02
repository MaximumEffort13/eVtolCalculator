using System.ComponentModel.DataAnnotations;

namespace ApiClient.DataTransferObjects.ApiRequests;

public class CreateDetailedDesign
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public CreateBatteryModel BatteryPack { get; set; }

    [Required]
    public CreateInverterModel Inverter { get; set; }

    [Required]
    public CreateMotorModel Motor { get; set; }

    [Required]
    public CreateBladeModel Blade { get; set; }

    [Required]
    public CreateFueslageModel Fuselage { get; set; }

    [Required]
    public CreateMissionParametersModel MissionParameters { get; set; }

    [Required]
    public int MotorQuantity { get; set; }

    [Required]
    public int BladeQuantity { get; set; }

}
