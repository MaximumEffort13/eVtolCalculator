using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Motors;

public sealed record CreateMotorCommand(Guid UserId, MotorInsert Motor) : ICommand<MotorDto>;


public sealed record MotorInsert(
    string Name,
    double VoltageRating_V,
    double CurrentRating_A,
    double Weight_kg,
    double Kv);