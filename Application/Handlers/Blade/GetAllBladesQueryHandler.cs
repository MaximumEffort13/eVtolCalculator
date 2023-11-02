using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Blade;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Blade;

internal sealed class GetAllBladesQueryHandler : IQueryHandler<GetAllBladesQuery, IEnumerable<BladeDto>>
{
    private readonly IBladeRepository _bladeRepository;

    public GetAllBladesQueryHandler(IBladeRepository bladeRepository)
    {
        _bladeRepository = bladeRepository;
    }

    public async Task<Result<IEnumerable<BladeDto>>> Handle(GetAllBladesQuery request, CancellationToken cancellationToken)
    {
        var blades = await _bladeRepository.GetAllAsync(cancellationToken);

        List<BladeDto> dtoBlades = new ();

        blades.ForEach(a => dtoBlades.Add(BladeDtoMapper.Map(a)));

        return dtoBlades;
    }
}
