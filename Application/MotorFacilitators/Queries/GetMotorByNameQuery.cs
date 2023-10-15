using Application.Abstractions;
using Application.DTO;

namespace Application.MotorFacilitators.Queries;
public sealed record GetMotorByNameQuery(string Name) : IQuery<MotorDto>;
