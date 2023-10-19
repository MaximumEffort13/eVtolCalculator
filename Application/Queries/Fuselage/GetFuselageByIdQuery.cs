using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Fuselage;
public sealed record GetFuselageByIdQuery(Guid Id) : IQuery<FuselageDto>;
