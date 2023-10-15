using Application.Abstractions;
using Application.DTO;
using Application.FuselageFacilitators.Queries;
using Application.Mappers;
using Domain.Abstractions;
using FluentResults;

namespace Application.FuselageFacilitators.Handlers;
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
