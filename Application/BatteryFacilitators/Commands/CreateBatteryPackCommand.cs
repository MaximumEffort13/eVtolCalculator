using Application.Abstractions;
using Application.DTO;
using Domain.Enums;

namespace Application.BatteryFacilitators.Commands;

public sealed record CreateBatteryPackCommand(
    string CellName,
    double CellVoltage,
    double CellCurrent,
    double CellWeight,
    double CellCapacity,
    int NumberOfCellsConnectedInSeries,
    int NumberOFCellsConnectedInParallel,
    int ModuleCountConnectedInSeries,
    int ModuleCountConnectedInParallel,
    double MiscellaneousWeigh,
    string PackName) : ICommand<BatteryPackDto>;
