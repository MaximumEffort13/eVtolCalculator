using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Mission;
public sealed record CreateMissionParameterCommand(Guid UserId, MissionParameterInsert Mission) : ICommand<MissionParameterDto>;

public sealed record MissionParameterInsert(
    double TotalStructureWeight_kg,
    double PayloadWeight_kg,
    double FlightTimeInMinutes);