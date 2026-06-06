using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Inverter;
public sealed record GetInverterByIdQuery(Guid Id, Guid UserId) : IQuery<InverterDto>;
