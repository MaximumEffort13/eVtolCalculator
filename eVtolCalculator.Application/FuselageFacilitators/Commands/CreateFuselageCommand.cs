using Application.Abstractions;
using Application.DTO;

namespace Application.FuselageFacilitators.Commands;

public sealed record CreateFuselageCommand(double Weight) : ICommand<FuselageDto>;
