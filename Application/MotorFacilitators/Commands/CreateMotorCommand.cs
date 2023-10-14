using Application.Abstractions;
using Application.DTO;

namespace Application.MotorFacilitators.Commands;

public sealed record CreateMotorCommand(
    string Name,
    double VoltageRating,
    double CurrentRating,
    double Weight,
    double Kv) : ICommand<MotorDto>;
