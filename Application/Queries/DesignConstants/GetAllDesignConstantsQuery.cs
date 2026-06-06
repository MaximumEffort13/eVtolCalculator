using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.DesignConstants;

public sealed record GetAllDesignConstantsQuery() : IQuery<List<DesignConsantsDto>>;
