using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Inverter;

public sealed record CreateInverterCommand(
    string Name,
    double VoltageRating_V,
    double CurrentRating_A,
    double Weight_kg) : ICommand<InverterDto>;
