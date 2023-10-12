using Application.Abstractions;
using Application.DTO;

namespace Application.InverterFacilitators.Commands;

public sealed record CreateInverterCommand(
    double VoltageRating,
    double CurrentRating,
    double Weight) : ICommand<InverterDto>;
