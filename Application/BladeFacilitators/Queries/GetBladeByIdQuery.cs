using Application.Abstractions;
using Application.DTO;

namespace Application.BladeFacilitators.Queries;

public sealed record GetBladeByIdQuery(Guid Id) : IQuery<BladeDto>;
