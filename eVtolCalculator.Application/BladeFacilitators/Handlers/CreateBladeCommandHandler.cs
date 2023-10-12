using Application.Abstractions;
using Application.BladeFacilitators.Commands;
using Application.DTO;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities.DetailedDesign;
using Domain.Enums;
using Domain.Primitives;
using FluentResults;

namespace Application.BladeFacilitators.Handlers;

internal sealed class CreateBladeCommandHandler : ICommandHandler<CreateBladeCommand, BladeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBladeRepository _bladeRepository;

    public CreateBladeCommandHandler(IUnitOfWork unitOfWork, IBladeRepository bladeRepository)
    {
        _unitOfWork = unitOfWork;
        _bladeRepository = bladeRepository;
    }

    public async Task<Result<BladeDto>> Handle(CreateBladeCommand request, CancellationToken cancellationToken)
    {
        MeasureandQuantity length = new(request.Length, SiPrefixes.Milli.Name + SiUnits.Meter.Name);
        MeasureandQuantity width = new(request.Widht, SiPrefixes.Milli.Name + SiUnits.Meter.Name);
        MeasureandQuantity thickness = new(request.Thickness, SiPrefixes.Milli.Name + SiUnits.Meter.Name);
        MeasureandQuantity weight = new(request.Weight, SiUnits.Mass.Name);
        MeasureandQuantity angleOfAttack = new(request.AngleOfAttack, SiUnits.Degress.Name);

        var blade = new Blade(Guid.NewGuid(), length, width, thickness, weight, angleOfAttack);

        _bladeRepository.Create(blade);
        await _unitOfWork.SaveChangesAsync();

        var response = BladeDtoMapper.Map(blade);
        return response;
    }
}
