using Application.Abstractions;
using Application.DTO;
using Domain.Enums;

namespace Application.BatteryFacilitators.Commands;

public sealed record CreateBatteryPackCommand(
    string CellName,
    double CellVoltage,
    double CellCurrent,
    double CellWeight_g,
    double CellCapacity,
    int NumberOfCellsConnectedInSeries,
    int NumberOfCellsConnectedInParallel,
    int ModuleCountConnectedInSeries,
    int ModuleCountConnectedInParallel,
    double MiscellaneousPackWeight_kg,
    string PackName) : ICommand<BatteryPackDto>;
