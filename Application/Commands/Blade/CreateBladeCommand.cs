using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Blade;

public sealed record CreateBladeCommand(
    string Name,
    double Length_mm,
    double Width_mm,
    double Thickness_mm,
    double Weight_g,
    double AngleOfAttack) : ICommand<BladeDto>;
