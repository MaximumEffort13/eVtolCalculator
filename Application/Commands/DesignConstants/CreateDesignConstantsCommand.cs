using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.DesignConstants;

public sealed record CreateDesignConstantsCommand(string Name, double Value) : ICommand<DesignConsantsDto>;
