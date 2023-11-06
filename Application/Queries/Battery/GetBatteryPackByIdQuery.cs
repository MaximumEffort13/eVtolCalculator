using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Battery;
public sealed record GetBatteryPackByIdQuery(Guid Id, Guid UserId) : IQuery<BatteryPackDto>;
