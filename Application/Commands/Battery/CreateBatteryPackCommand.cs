using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Battery;

public sealed record CreateBatteryPackCommand(
    string CellName,
    double CellVoltage_V,
    double CellCurrent_mA,
    double CellWeight_g,
    double CellCapacity_mWh,
    int NumberOfCellsConnectedInSeries,
    int NumberOfCellsConnectedInParallel,
    int ModuleCountConnectedInSeries,
    int ModuleCountConnectedInParallel,
    double MiscellaneousPackWeight_kg,
    string PackName) : ICommand<BatteryPackDto>;
