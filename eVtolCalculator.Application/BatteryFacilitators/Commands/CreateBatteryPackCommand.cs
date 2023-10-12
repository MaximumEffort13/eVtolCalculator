using Application.Abstractions;
using Application.DTO;
using Domain.Enums;

namespace Application.BatteryFacilitators.Commands;

public sealed record CreateBatteryPackCommand(
    double CellVoltage,
    double CellCurrent,
    double CellWeight,
    double CellCapacity,
    int CellCount,
    ConnectionOrientation cellOrientationInModule,
    int ModuleCount,
    ConnectionOrientation ModuleOrientationInPack,
    double miscellaneousWeigh) : ICommand<BatteryPackDto>;
