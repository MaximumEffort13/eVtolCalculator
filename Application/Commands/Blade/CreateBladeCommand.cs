using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Blade;

public sealed record CreateBladeCommand(Guid UserId, BladeInsert Blade) : ICommand<BladeDto>;


public class BladeInsert
{
    public string Name { get; set; }
    public double Diameter_mm { get; set; }
    public double Width_mm { get; set; }
    public double Thickness_mm { get; set; }
    public double Weight_g { get; set; }
    public double AngleOfAttack { get; set; }
}