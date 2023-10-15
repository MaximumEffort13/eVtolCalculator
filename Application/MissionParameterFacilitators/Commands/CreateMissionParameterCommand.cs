using Application.Abstractions;
using Application.DTO;

namespace Application.MissionParameterFacilitators.Commands;
public sealed record CreateMissionParameterCommand(
    double TotalStructureWeight_kg,
    double PayloadWeight_kg,
    double FlightTimeInMinutes) : ICommand<MissionParameterDto>;
