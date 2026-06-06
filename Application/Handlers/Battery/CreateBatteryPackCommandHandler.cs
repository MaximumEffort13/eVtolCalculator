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
        MeasureandQuantity cellVoltage = new(Math.Round(request.Battery.CellVoltage_V, 4), SiUnits.Voltage.Name);
        MeasureandQuantity cellCapacity = new(Math.Round(request.Battery.CellCapacity_Ah,4), SiUnits.AmpHour.Name);
        MeasureandQuantity cellMass = new(Math.Round(request.Battery.CellMass_g, 4), SiUnits.Mass.Name);
        MeasureandQuantity cellEnergy = ElectricCalculations.CalculateEnergy(cellVoltage, cellCapacity);

        Cell cell = new(Guid.NewGuid(), request.Battery.CellName, cellVoltage, cellCapacity, cellEnergy, cellMass);

        BatteryModule module = new(Guid.NewGuid(), cell.Id, request.Battery.NumberOfCellsConnectedInSeries, request.Battery.NumberOfCellsConnectedInParallel)
        {
            Voltage = ElectricCalculations.CalculateVoltageFromUnitConnectionCount(cell.Voltage, request.Battery.NumberOfCellsConnectedInSeries),
            Capacity = ElectricCalculations.CalculateCapacityBaseOnUnitConnections(cell.Capacity, request.Battery.NumberOfCellsConnectedInParallel),
            Mass = MechanicalCalculations.CalculateBatteryModuleWeight(cellMass, request.Battery.NumberOfCellsConnectedInParallel * request.Battery.NumberOfCellsConnectedInSeries),
        };
        module.Energy = ElectricCalculations.CalculateEnergy(module.Voltage, module.Capacity);

        MeasureandQuantity miscellaneousBatteryPackWeight = new(Math.Round(request.Battery.MiscellaneousPackWeight_kg, 4), SiPrefixes.Kilo.Name + SiUnits.Mass.Name);

        BatteryPack batteryPack = new (
            Guid.NewGuid(),
            request.UserId,
            request.Battery.PackName,
            module.Id,
            request.Battery.NumberOfModulesConnectedInSeries,
            request.Battery.NumberOfModulesConnectedInParallel,
            miscellaneousBatteryPackWeight)
        {
            Voltage = ElectricCalculations.CalculateVoltageFromUnitConnectionCount(module.Voltage, request.Battery.NumberOfModulesConnectedInSeries),
            Capacity = ElectricCalculations.CalculateCapacityBaseOnUnitConnections(module.Capacity, request.Battery.NumberOfModulesConnectedInParallel),
            Mass = MechanicalCalculations.CalculateBatteryPackWeightUsingBatteryModule(module.Mass, request.Battery.NumberOfModulesConnectedInSeries * request.Battery.NumberOfModulesConnectedInParallel, miscellaneousBatteryPackWeight),
        };

        batteryPack.Energy = ElectricCalculations.CalculateEnergy(batteryPack.Voltage, batteryPack.Capacity);

        batteryPack.SpecificEnergy = ElectricCalculations.CalculateSpecificEnergy(batteryPack.Energy, batteryPack.Mass);

        _batteryCell.CreateBatteryCell(cell);
        _batteryModule.CreateBatteryModule(module);
        _batteryPack.CreateBatteryPack(batteryPack);

        var response = BatteryPackDtoMapper.Map(batteryPack);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;

    }
}
