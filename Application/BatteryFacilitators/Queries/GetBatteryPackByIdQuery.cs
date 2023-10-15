using Application.Abstractions;
using Application.DTO;

namespace Application.BatteryFacilitators.Queries;
public sealed record GetBatteryPackByIdQuery(Guid Id) : IQuery<BatteryPackDto>;
