using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Mission;
public sealed record CreateMissionParameterCommand(
    double TotalStructureWeight_kg,
    double PayloadWeight_kg,
    double FlightTimeInMinutes) : ICommand<MissionParameterDto>;
