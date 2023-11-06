using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Blade;

public sealed record GetBladeByNameQuery(string Name, Guid UserId) : IQuery<BladeDto>;

