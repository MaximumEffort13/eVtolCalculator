using Application.Abstractions;
using Application.BatteryFacilitators.Commands;
using Application.DTO;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities.DetailedDesign.Battery;
using Domain.EntityCalculations;
using Domain.Enums;
using Domain.Primitives;
using FluentResults;

namespace Application.BatteryFacilitators.Handlers;

internal class CreateBatteryPackCommandHandler : ICommandHandler<CreateBatteryPackCommand, BatteryPackDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBatteryPackRepository _batteryPack;
    private readonly IBatteryModuleRepository _batteryModule;
    private readonly IBatteryCellRepository _batteryCell;

    public CreateBatteryPackCommandHandler(
        IUnitOfWork unitOfWork,
        IBatteryPackRepository batteryPack,
        IBatteryModuleRepository batteryModule,
        IBatteryCellRepository batteryCell)
    {
        _unitOfWork = unitOfWork;
        _batteryPack = batteryPack;
        _batteryModule = batteryModule;
        _batteryCell = batteryCell;
    }

    public async Task<Result<BatteryPackDto>> Handle(CreateBatteryPackCommand request, CancellationToken cancellationToken)
    {
        MeasureandQuantity cellVoltage = new(request.CellVoltage_V, SiUnits.Voltage.Name);
        MeasureandQuantity cellCurrent = new(request.CellCurrent_mA, SiUnits.Current.Name);
        MeasureandQuantity cellCapacity = new(request.CellCapacity_mWh, SiUnits.WattHour.Name);
        MeasureandQuantity cellWeight = new(request.CellWeight_g, SiUnits.Mass.Name);

        Cell cell = new(Guid.NewGuid(), request.CellName, cellVoltage, cellCapacity, cellCurrent, cellWeight);
        BatteryModule module = new(Guid.NewGuid(), cell.Id, request.NumberOfCellsConnectedInSeries, request.NumberOfCellsConnectedInParallel)
        {
            Voltage = ElectricCalculations.CalculateVoltageFromUnitConnectionCount(cell.Voltage, request.NumberOfCellsConnectedInSeries),
            Current = ElectricCalculations.CalculateCurrentFromUnitConnectionCount(cell.Current, request.NumberOfCellsConnectedInParallel),
            Capacity = ElectricCalculations.CalculateCapacityBaseOnUnitConnections(cell.Capacity, request.NumberOfCellsConnectedInParallel),
            Power = ElectricCalculations.CalculatePower(cellVoltage, cellCurrent),
            Weight = MechanicalCalculations.CalculateBatteryModuleWeight(cellWeight, request.NumberOfCellsConnectedInParallel + request.NumberOfCellsConnectedInSeries),
        };

        MeasureandQuantity miscellaneousBatteryPackWeight = new(request.MiscellaneousPackWeight_kg, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);

        BatteryPack batteryPack = new(
            Guid.NewGuid(),
            request.PackName,
            module.Id,
            request.ModuleCountConnectedInSeries,
            request.ModuleCountConnectedInParallel,
            miscellaneousBatteryPackWeight)
        {
            Current = ElectricCalculations.CalculateCurrentFromUnitConnectionCount(module.Current, request.ModuleCountConnectedInParallel),
            Voltage = ElectricCalculations.CalculateVoltageFromUnitConnectionCount(module.Voltage, request.NumberOfCellsConnectedInSeries),
            Capacity = ElectricCalculations.CalculateCapacityBaseOnUnitConnections(module.Capacity, request.ModuleCountConnectedInParallel),
            Power = ElectricCalculations.CalculatePower(module.Voltage, module.Current),
            Weight = MechanicalCalculations.CalculateBatteryPackWeightUsingBatteryModule(module.Weight, request.ModuleCountConnectedInSeries + request.ModuleCountConnectedInParallel),
        };

        batteryPack.SpecificEnergy = ElectricCalculations.CalculateSpecificEnergy(batteryPack.Capacity, batteryPack.Weight);

        _batteryCell.CreateBatteryCell(cell);
        _batteryModule.CreateBatteryModule(module);
        _batteryPack.CreateBatteryPack(batteryPack);

        var response = BatteryPackDtoMapper.Map(batteryPack);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;

    }
}
