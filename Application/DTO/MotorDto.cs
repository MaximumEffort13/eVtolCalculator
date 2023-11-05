using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

[Serializable]
public class MotorDto
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string VoltageRating { get; set; }

    [Required]
    public string CurrentRating { get; set; }

    [Required]
    public string Weight { get; set; }

    [Required]
    public string PowerToWeightRatio { get; set; }

    [Required]
    public string RpmValue { get; set; }
}


//public class MotorDtoListRoot
//{
//    public List<MotorDto> MotorDtos { get; set; } = new List<MotorDto>();
//}