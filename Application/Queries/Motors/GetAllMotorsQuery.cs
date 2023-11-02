using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Motors;

public sealed record GetAllMotorsQuery() : IQuery<IEnumerable<MotorDto>>;
