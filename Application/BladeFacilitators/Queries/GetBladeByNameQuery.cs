using Application.Abstractions;
using Application.DTO;

namespace Application.BladeFacilitators.Queries;

public sealed record GetBladeByNameQuery(string Name) : IQuery<BladeDto>;

