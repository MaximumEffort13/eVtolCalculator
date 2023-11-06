using Application.Abstractions;
using Application.Commands.Battery;
using Application.DTO;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities.DetailedDesign.Battery;
using Domain.EntityCalculations;
using Domain.Enums;
using Domain.Primitives;
using FluentResults;

namespace Application.Handlers.Battery;

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
        MeasureandQuantity cellVoltage = new(request.Battery.CellVoltage_V, SiUnits.Voltage.Name);
        MeasureandQuantity cellCurrent = new(request.Battery.CellCurrent_mA, SiPrefixes.Milli.Name + SiUnits.Current.Name);
        MeasureandQuantity cellCapacity = new(request.Battery.CellCapacity_mWh, SiPrefixes.Milli.Name + SiUnits.WattHour.Name);
        MeasureandQuantity cellWeight = new(request.Battery.CellWeight_g, SiUnits.Mass.Name);

        Cell cell = new(Guid.NewGuid(), request.Battery.CellName, cellVoltage, cellCapacity, cellCurrent, cellWeight);
        BatteryModule module = new(Guid.NewGuid(), cell.Id, request.Battery.NumberOfCellsConnectedInSeries, request.Battery.NumberOfCellsConnectedInParallel)
        {
            Voltage = ElectricCalculations.CalculateVoltageFromUnitConnectionCount(cell.Voltage, request.Battery.NumberOfCellsConnectedInSeries),
            Current = ElectricCalculations.CalculateCurrentFromUnitConnectionCount(cell.Current, request.Battery.NumberOfCellsConnectedInParallel),
            Capacity = ElectricCalculations.CalculateCapacityBaseOnUnitConnections(cell.Capacity, request.Battery.NumberOfCellsConnectedInParallel, request.Battery.NumberOfCellsConnectedInSeries),
            Weight = MechanicalCalculations.CalculateBatteryModuleWeight(cellWeight, request.Battery.NumberOfCellsConnectedInParallel * request.Battery.NumberOfCellsConnectedInSeries),
        };
        module.Power = ElectricCalculations.CalculatePower(module.Voltage, module.Current);

        MeasureandQuantity miscellaneousBatteryPackWeight = new(request.Battery.MiscellaneousPackWeight_kg, SiPrefixes.Kilo.Name + SiUnits.Mass.Name);

        BatteryPack batteryPack = new (
            request.UserId,
            Guid.NewGuid(),
            request.Battery.PackName,
            module.Id,
            request.Battery.NumberOfModulesConnectedInSeries,
            request.Battery.NumberOfModulesConnectedInParallel,
            miscellaneousBatteryPackWeight)
        {
            Current = ElectricCalculations.CalculateCurrentFromUnitConnectionCount(module.Current, request.Battery.NumberOfModulesConnectedInParallel),
            Voltage = ElectricCalculations.CalculateVoltageFromUnitConnectionCount(module.Voltage, request.Battery.NumberOfCellsConnectedInSeries),
            Capacity = ElectricCalculations.CalculateCapacityBaseOnUnitConnections(module.Capacity, request.Battery.NumberOfModulesConnectedInParallel, request.Battery.NumberOfModulesConnectedInSeries),
            Weight = MechanicalCalculations.CalculateBatteryPackWeightUsingBatteryModule(module.Weight, request.Battery.NumberOfModulesConnectedInSeries * request.Battery.NumberOfModulesConnectedInParallel, miscellaneousBatteryPackWeight),
        };

        batteryPack.Power = ElectricCalculations.CalculatePower(batteryPack.Voltage, batteryPack.Current);

        batteryPack.SpecificEnergy = ElectricCalculations.CalculateSpecificEnergy(batteryPack.Capacity, batteryPack.Weight);

        _batteryCell.CreateBatteryCell(cell);
        _batteryModule.CreateBatteryModule(module);
        _batteryPack.CreateBatteryPack(batteryPack);

        var response = BatteryPackDtoMapper.Map(batteryPack);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;

    }
}
