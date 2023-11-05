using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.DetailDesign;

public sealed record GetDetailDesignByNameQuery(string Name) : IQuery<ElectricVtolDesignDto>;