using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Fuselage;

public sealed record CreateFuselageCommand(double Weight_kg) : ICommand<FuselageDto>;
