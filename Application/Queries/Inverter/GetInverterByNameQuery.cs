using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Inverter;
public sealed record GetInverterByNameQuery(string Name, Guid UserId) : IQuery<InverterDto>;
