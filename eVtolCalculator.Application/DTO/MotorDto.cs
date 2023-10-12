using Domain.Primitives;

namespace Application.DTO;

public class MotorDto
{
    public string VoltageRating { get; set; }
    public string CurrentRating { get; set; }
    public string Weight { get; set; }
    public string PowerToWeightRatio { get; set; }
}
