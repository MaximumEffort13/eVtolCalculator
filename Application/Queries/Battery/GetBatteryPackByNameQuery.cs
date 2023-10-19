using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Battery;

public sealed record GetBatteryPackByNameQuery(string Name) : IQuery<BatteryPackDto>;
