using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Blade;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Blade;
internal sealed class GetBladeByNameQueryHandler : IQueryHandler<GetBladeByNameQuery, BladeDto>
{
    private readonly IBladeRepository _bladeRepository;

    public GetBladeByNameQueryHandler(IBladeRepository bladeRepository)
    {
        _bladeRepository = bladeRepository;
    }

    public async Task<Result<BladeDto>> Handle(GetBladeByNameQuery request, CancellationToken cancellationToken)
    {
        var blade = await _bladeRepository.GetByNameAsync(request.Name, request.UserId, cancellationToken);

        var response = BladeDtoMapper.Map(blade);

        return response;
    }
}
