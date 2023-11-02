using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Battery;

public sealed record GetAllBatteryPacksQuery() : IQuery<IEnumerable<BatteryPackDto>>;
