using Application.Abstractions;
using Application.DTO;

namespace Application.BladeFacilitators.Commands;

public sealed record CreateBladeCommand(
    string Name,
    double Length,
    double Widht,
    double Thickness,
    double Weight,
    double AngleOfAttack) : ICommand<BladeDto>;
