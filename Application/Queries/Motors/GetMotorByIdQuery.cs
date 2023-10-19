using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Motors;
public sealed record GetMotorByIdQuery(Guid Id) : IQuery<MotorDto>;
