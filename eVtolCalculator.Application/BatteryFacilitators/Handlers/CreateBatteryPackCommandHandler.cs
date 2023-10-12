using Application.Abstractions;
using Application.BatteryFacilitators.Commands;
using Application.DTO;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities.DetailedDesign.Battery;
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
        MeasureandQuantity cellVoltage = new(request.CellVoltage, SiUnits.Voltage.Name);
        MeasureandQuantity cellCurrent = new(request.CellCurrent, SiUnits.Current.Name);
        MeasureandQuantity cellCapacity = new(request.CellCapacity, SiUnits.WattHour.Name);
        MeasureandQuantity cellWeight = new(request.CellWeight, SiUnits.Mass.Name);

        Cell cell = new(Guid.NewGuid(), cellVoltage, cellCapacity, cellCurrent, cellWeight);
        BatteryModule module = new(Guid.NewGuid(), cell, request.CellCount, request.cellOrientationInModule);
        BatteryPack batteryPack = new(Guid.NewGuid(), module, request.ModuleCount, request.ModuleOrientationInPack, request.miscellaneousWeigh);

        _batteryCell.CreateBatteryCell(cell);
        _batteryModule.CreateBatteryModule(module);
        _batteryPack.CreateBatteryPack(batteryPack);

        var response = BatteryPackDtoMapper.Map(batteryPack);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return response;

    }
}
