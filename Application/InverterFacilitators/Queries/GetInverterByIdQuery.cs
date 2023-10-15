using Application.Abstractions;
using Application.DTO;

namespace Application.InverterFacilitators.Queries;
public sealed record GetInverterByIdQuery(Guid Id) : IQuery<InverterDto>;
