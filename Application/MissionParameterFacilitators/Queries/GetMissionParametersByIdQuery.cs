using Application.Abstractions;
using Application.DTO;

namespace Application.MissionParameterFacilitators.Queries;
public sealed record GetMissionParametersByIdQuery(Guid Id) : IQuery<MissionParameterDto>;
