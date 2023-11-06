using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Inverter;

public sealed record CreateInverterCommand(Guid UserId, InverterInsert Inverter) : ICommand<InverterDto>;


public sealed record InverterInsert(
    string Name,
    double VoltageRating_V,
    double CurrentRating_A,
    double Weight_kg);