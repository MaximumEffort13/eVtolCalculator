using Application.Abstractions;
using Application.DTO;

namespace Application.FuselageFacilitators.Queries;
public sealed record GetFuselageByIdQuery(Guid Id) : IQuery<FuselageDto>;
