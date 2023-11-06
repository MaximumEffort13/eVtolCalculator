using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Fuselage;

public sealed record CreateFuselageCommand(Guid UserId, FuselageInsert Fuselage) : ICommand<FuselageDto>;

public sealed record FuselageInsert(double Weight_kg);
