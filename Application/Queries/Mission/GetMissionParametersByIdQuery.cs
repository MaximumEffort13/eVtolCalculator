using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Mission;
public sealed record GetMissionParametersByIdQuery(Guid Id, Guid UserId) : IQuery<MissionParameterDto>;
