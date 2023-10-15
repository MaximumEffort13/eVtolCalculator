using Application.Abstractions;
using Application.DTO;

namespace Application.MotorFacilitators.Queries;
public sealed record GetMotorByIdQuery(Guid Id) : IQuery<MotorDto>;
