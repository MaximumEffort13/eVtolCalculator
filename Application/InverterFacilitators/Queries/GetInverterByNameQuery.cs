using Application.Abstractions;
using Application.DTO;

namespace Application.InverterFacilitators.Queries;
public sealed record GetInverterByNameQuery(string Name) : IQuery<InverterDto>;
