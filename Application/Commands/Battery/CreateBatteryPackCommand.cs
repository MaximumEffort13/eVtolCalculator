using Application.Abstractions;
using Application.DTO;

namespace Application.Commands.Battery;

public sealed record CreateBatteryPackCommand(Guid UserId, BatteryPackInsert Battery) : ICommand<BatteryPackDto>;

public sealed record BatteryPackInsert(
    string CellName,
    double CellVoltage_V,
    double CellMass_g,
    double CellCapacity_Ah,
    int NumberOfCellsConnectedInSeries,
    int NumberOfCellsConnectedInParallel,
    int NumberOfModulesConnectedInSeries,
    int NumberOfModulesConnectedInParallel,
    double MiscellaneousPackWeight_kg,
    string PackName);