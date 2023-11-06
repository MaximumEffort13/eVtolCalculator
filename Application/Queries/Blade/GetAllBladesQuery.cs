using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Blade;

public sealed record GetAllBladesQuery(Guid UserId) : IQuery<List<BladeDto>>;
