using Application.Abstractions;
using Application.DTO;

namespace Application.BatteryFacilitators.Queries;

public sealed record GetBatteryPackByNameQuery(string Name) : IQuery<BatteryPackDto>;
