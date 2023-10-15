using Application.Abstractions;
using Application.DTO;

namespace Application.BladeFacilitators.Commands;

public sealed record CreateBladeCommand(
    string Name,
    double Length_mm,
    double Widht_mm,
    double Thickness_mm,
    double Weight_g,
    double AngleOfAttack) : ICommand<BladeDto>;
