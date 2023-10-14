using Application.Abstractions;
using Application.DTO;

namespace Application.InverterFacilitators.Commands;

public sealed record CreateInverterCommand(
    string Name,
    double VoltageRating,
    double CurrentRating,
    double Weight) : ICommand<InverterDto>;
