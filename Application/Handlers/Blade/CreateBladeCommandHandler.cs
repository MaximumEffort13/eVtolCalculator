using Application.Abstractions;
using Application.Commands.Blade;
using Application.DTO;
using Application.Mappers;
using Domain.Abstractions;
using Domain.Entities.DetailedDesign;
using Domain.Enums;
using Domain.Primitives;
using FluentResults;

namespace Application.Handlers.Blade;

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
        MeasureandQuantity length = new(request.Blade.Length_mm, SiPrefixes.Milli.Name + SiUnits.Meter.Name);
        MeasureandQuantity width = new(request.Blade.Width_mm, SiPrefixes.Milli.Name + SiUnits.Meter.Name);
        MeasureandQuantity thickness = new(request.Blade.Thickness_mm, SiPrefixes.Milli.Name + SiUnits.Meter.Name);
        MeasureandQuantity weight = new(request.Blade.Weight_g, SiUnits.Mass.Name);
        MeasureandQuantity angleOfAttack = new(request.Blade.AngleOfAttack, SiUnits.Degress.Name);

        var blade = new BladeEntity(Guid.NewGuid(), request.UserId, request.Blade.Name, length, width, thickness, weight, angleOfAttack);

        _bladeRepository.Create(blade);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = BladeDtoMapper.Map(blade);
        return response;
    }
}
