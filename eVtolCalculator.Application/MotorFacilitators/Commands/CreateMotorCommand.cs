using Application.Abstractions;
using Application.DTO;

namespace Application.MotorFacilitators.Commands;

public sealed record CreateMotorCommand(
    double VoltageRating,
    double CurrentRating,
    double Weight,
    double Kv) : ICommand<MotorDto>;
