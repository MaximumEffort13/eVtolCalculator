using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.Inverter;

public sealed record GetAllInvertersQuery() : IQuery<List<InverterDto>>;
