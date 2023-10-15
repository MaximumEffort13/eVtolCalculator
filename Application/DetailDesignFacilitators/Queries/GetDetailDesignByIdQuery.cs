using Application.Abstractions;
using Application.DTO;

namespace Application.DetailDesignFacilitators.Queries;

public sealed record GetDetailDesignByIdQuery(Guid Id) : IQuery<ElectricVtolDesignDto>;
