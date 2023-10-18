using Application.Abstractions;
using Application.DTO;

namespace Application.InverterFacilitators.Commands;

public sealed record CreateInverterCommand(
    string Name,
    double VoltageRating_V,
    double CurrentRating_A,
    double Weight_kg) : ICommand<InverterDto>;
