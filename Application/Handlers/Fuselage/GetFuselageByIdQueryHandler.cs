using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.Fuselage;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.Fuselage;
internal class GetFuselageByIdQueryHandler : IQueryHandler<GetFuselageByIdQuery, FuselageDto>
{
    private readonly IFuselageRepository _fuselageRepository;

    public GetFuselageByIdQueryHandler(IFuselageRepository fuselageRepository)
    {
        _fuselageRepository = fuselageRepository;
    }

    public async Task<Result<FuselageDto>> Handle(GetFuselageByIdQuery request, CancellationToken cancellationToken)
    {
        var fuselage = await _fuselageRepository.GetByIdAsync(request.Id, cancellationToken);

        var response = FuselageDtoMapper.Map(fuselage);

        return response;
    }
}
