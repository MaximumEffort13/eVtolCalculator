using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Blade;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Blade;
internal sealed class GetBladeByIdQueryHandler : IQueryHandler<GetBladeByIdQuery, BladeDto>
{
    private readonly IBladeRepository _bladeRepository;

    public GetBladeByIdQueryHandler(IBladeRepository bladeRepository)
    {
        _bladeRepository = bladeRepository;
    }

    public async Task<Result<BladeDto>> Handle(GetBladeByIdQuery request, CancellationToken cancellationToken)
    {
        var blade = await _bladeRepository.GetByIdAsync(request.Id, cancellationToken);

        var response = BladeDtoMapper.Map(blade);

        return response;
    }
}
