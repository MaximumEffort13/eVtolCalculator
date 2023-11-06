using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Blade;

public sealed record GetBladeByIdQuery(Guid Id, Guid UserId) : IQuery<BladeDto>;
