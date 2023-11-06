using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Motors;
public sealed record GetMotorByNameQuery(string Name, Guid UserId) : IQuery<MotorDto>;
