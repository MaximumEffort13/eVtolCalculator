using Application.Abstractions;
using Application.DTO;

namespace Application.Queries.DesignConstants;

public sealed record GetDesignConstantByIdQuery(string Id) : IQuery<DesignConsantsDto>;
