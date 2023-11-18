using Application.Abstractions;
using Application.DTO;
using Application.Mappers;
using Application.Queries.DesignConstants;
using Domain.Abstractions;
using FluentResults;

namespace Application.Handlers.DesignConstants;

internal sealed class GetDesignConstantsByIdQueryHandler(IDesignConstantsRepository designConstants) : IQueryHandler<GetDesignConstantByIdQuery, DesignConsantsDto>
{
    private readonly IDesignConstantsRepository _designConstants = designConstants;

    public async Task<Result<DesignConsantsDto>> Handle(GetDesignConstantByIdQuery request, CancellationToken cancellationToken)
    {
        var designConst = await _designConstants.GetByIdAsync(Guid.Parse(request.Id), cancellationToken);

        var desingConstDto = DesignConstantsDtoMapper.Map(designConst);

        return desingConstDto;
    }
}
