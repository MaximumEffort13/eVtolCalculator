using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.MissionParameterFacilitators.Queries;
using Domain.Abstractions;
using FluentResults;

namespace Application.MissionParameterFacilitators.Handlers;
internal class GetMissionParametersByIdQueryHandler : IQueryHandler<GetMissionParametersByIdQuery, MissionParameterDto>
{
    private readonly IMissionParameterRepository _missionParameterRepository;

    public GetMissionParametersByIdQueryHandler(IMissionParameterRepository missionParameterRepository)
    {
        _missionParameterRepository = missionParameterRepository;
    }

    public async Task<Result<MissionParameterDto>> Handle(GetMissionParametersByIdQuery request, CancellationToken cancellationToken)
    {
        var missionParameter = await _missionParameterRepository.GetByIdAsync(request.Id, cancellationToken);

        var response = MissionParameterDtoMapper.Map(missionParameter);

        return response;
    }
}
