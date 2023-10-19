using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.DetailDesign;

public sealed record GetDetailDesignByIdQuery(Guid Id) : IQuery<ElectricVtolDesignDto>;
