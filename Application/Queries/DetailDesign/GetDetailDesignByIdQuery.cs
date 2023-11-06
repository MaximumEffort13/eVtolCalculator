using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.DetailDesign;

public sealed record GetDetailDesignByIdQuery(Guid Id, Guid UserId) : IQuery<ElectricVtolDesignDto>;
