using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.DetailDesign;

public sealed record GetAllDetailDesignsQuery(Guid UserId) : IQuery<List<ElectricVtolDesignDto>>;
